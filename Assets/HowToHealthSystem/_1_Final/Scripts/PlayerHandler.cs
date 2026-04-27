using System;
using UnityEngine;
using V_AnimationSystem;
using CodeMonkey.Utils;

namespace CodeMoney_HowToHealthSystem_1_Final {
    /*
     * Player movement with WASD keys
     * Attack with Space
     * */
    public class PlayerHandler : MonoBehaviour {
        
        public static PlayerHandler CreatePlayer(Func<Vector3, EnemyHandler> getClosestEnemyHandlerFunc) {
            Transform playerTransform = Instantiate(GameAssets.i.pfPlayerTransform, new Vector3(0, 0), Quaternion.identity);
            
            HealthSystem healthSystem = new HealthSystem(100);
            HealthBar healthBar = Instantiate(GameAssets.i.pfHealthBar, new Vector3(0, 10), Quaternion.identity, playerTransform).GetComponent<HealthBar>();
            healthBar.Setup(healthSystem);

            PlayerHandler playerHandler = playerTransform.GetComponent<PlayerHandler>();
            playerHandler.Setup(healthSystem, getClosestEnemyHandlerFunc);

            return playerHandler;
        }
        
        public event EventHandler OnDead;

        private const float speed = 50f;
        
        private V_UnitSkeleton unitSkeleton;
        private V_UnitAnimation unitAnimation;
        private HealthSystem healthSystem;
        private Func<Vector3, EnemyHandler> getClosestEnemyHandlerFunc;
        private Vector3 lastMoveDir;
        private State state;

        private enum State {
            Normal,
            Busy,
            Dead,
        }

        private void Setup(HealthSystem healthSystem, Func<Vector3, EnemyHandler> getClosestEnemyHandlerFunc) {
            this.healthSystem = healthSystem;
            this.getClosestEnemyHandlerFunc = getClosestEnemyHandlerFunc;

            healthSystem.OnDead += HealthSystem_OnDead;
        }

        private void HealthSystem_OnDead(object sender, EventArgs e) {
            state = State.Dead;
            gameObject.SetActive(false);
            if (OnDead != null) OnDead(this, EventArgs.Empty);
        }

        private void Start() {
            Transform bodyTransform = transform.Find("Body");
            unitSkeleton = new V_UnitSkeleton(1f, bodyTransform.TransformPoint, (Mesh mesh) => bodyTransform.GetComponent<MeshFilter>().mesh = mesh);
            unitAnimation = new V_UnitAnimation(unitSkeleton);
            unitAnimation.PlayAnim(GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Idle, 1f, null, null, null);

            state = State.Normal;
        }

        private void Update() {
            switch (state) {
            case State.Normal:
                HandleMovement();
                HandleAttack();
                break;
            case State.Busy:
                HandleAttack();
                break;
            case State.Dead:
                break;
            }
            unitSkeleton.Update(Time.deltaTime);
        }

        private void HandleMovement() {
            float moveX = 0;
            float moveY = 0;

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                moveY = 1;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                moveY = -1;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                moveX = -1;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                moveX = 1;
            }

            Vector3 moveDir = new Vector3(moveX, moveY).normalized;
            lastMoveDir = moveDir;
            bool isIdle = moveX == 0 && moveY == 0;
            if (isIdle) {
                lastMoveDir = Vector3.down;
                unitAnimation.PlayAnim(GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Idle, lastMoveDir, 1f, null, null, null);
            } else {
                unitAnimation.PlayAnim(GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Walk, lastMoveDir, .75f, null, null, null);
                Dirt_Handler.SpawnInterval(GetPosition() + new Vector3(0, -4), lastMoveDir * -1);
            }
            transform.position = transform.position + moveDir * speed * Time.deltaTime;
        }

        private void HandleAttack() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                // Attack
                SetStateBusy();

                Vector3 attackDir = lastMoveDir;
                EnemyHandler enemyHandler = getClosestEnemyHandlerFunc(GetPosition() + attackDir * 4f);
                if (enemyHandler != null) {
                    // Has a target nearby
                    attackDir = (enemyHandler.GetPosition() - GetPosition()).normalized;
                    // Test if too close to dash
                    const float tooCloseToDashDistance = 10f;
                    if (Vector3.Distance(enemyHandler.GetPosition(), GetPosition()) > tooCloseToDashDistance) {
                        // Dash towards attack distance
                        transform.position = transform.position + attackDir * 4f;
                    }
                    // Is enemy within attack range?
                    const float attackDistance = 30f;
                    if (Vector3.Distance(enemyHandler.GetPosition(), GetPosition()) < attackDistance) {
                        // Close enough to damage
                        enemyHandler.GetHealthSystem().Damage(34);
                        Blood_Handler.SpawnBlood(enemyHandler.GetPosition(), attackDir);
                    }
                } else {
                    // No nearby target
                    transform.position = transform.position + attackDir * 4f;
                }
                lastMoveDir = attackDir;

                Transform swordSlashTransform = Instantiate(GameAssets.i.pfSwordSlash, GetPosition() + attackDir * 13f, Quaternion.Euler(0, 0, UtilsClass.GetAngleFromVector(attackDir)));
                swordSlashTransform.GetComponent<SpriteAnimator>().onLoop = () => Destroy(swordSlashTransform.gameObject);

                UnitAnimType activeAnimType = unitAnimation.GetActiveAnimType();
                if (activeAnimType == GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Sword) {
                    swordSlashTransform.localScale = new Vector3(swordSlashTransform.localScale.x, swordSlashTransform.localScale.y * -1, swordSlashTransform.localScale.z);
                    unitAnimation.PlayAnimForced(GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Sword2, lastMoveDir, 1f, (UnitAnim unitAnim) => SetStateNormal(), null, null);
                } else {
                    unitAnimation.PlayAnimForced(GameAssets.UnitAnimTypeEnum.dSwordTwoHandedBack_Sword, lastMoveDir, 1f, (UnitAnim unitAnim) => SetStateNormal(), null, null);
                }
            }
        }

        private void SetStateBusy() {
            state = State.Busy;
        }

        private void SetStateNormal() {
            state = State.Normal;
        }

        public bool IsDead() {
            return state == State.Dead;
        }

        public Vector3 GetPosition() {
            return transform.position;
        }

        public HealthSystem GetHealthSystem() {
            return healthSystem;
        }



        
    }

}