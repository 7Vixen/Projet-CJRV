using System;
using UnityEngine;
using V_AnimationSystem;

namespace CodeMoney_HowToHealthSystem_1_Final {
    /*
     * Player movement with WASD keys
     * Attack with Space
     * */
    public class EnemyHandler : MonoBehaviour {

        public static EnemyHandler CreateEnemy(Vector3 spawnPosition, PlayerHandler playerHandler) {
            Transform enemyTransform = Instantiate(GameAssets.i.pfEnemyTransform, spawnPosition, Quaternion.identity);
            EnemyHandler enemyHandler = enemyTransform.GetComponent<EnemyHandler>();

            HealthSystem healthSystem = new HealthSystem(100);
            HealthBar healthBar = Instantiate(GameAssets.i.pfHealthBar, spawnPosition + new Vector3(0, 10), Quaternion.identity, enemyTransform).GetComponent<HealthBar>();
            healthBar.Setup(healthSystem);

            enemyHandler.Setup(playerHandler, healthSystem);

            return enemyHandler;
        }

        public event EventHandler OnDead;

        private const float speed = 25f;
        
        private PlayerHandler playerHandler;
        private HealthSystem healthSystem;
        private V_UnitSkeleton unitSkeleton;
        private V_UnitAnimation unitAnimation;
        private Vector3 lastMoveDir;
        private State state;

        private enum State {
            Normal,
            Busy,
        }

        private void Setup(PlayerHandler playerHandler, HealthSystem healthSystem) {
            this.playerHandler = playerHandler;
            this.healthSystem = healthSystem;

            healthSystem.OnDead += HealthSystem_OnDead;
        }

        private void Start() {
            Transform bodyTransform = transform.Find("Body");
            unitSkeleton = new V_UnitSkeleton(1f, bodyTransform.TransformPoint, (Mesh mesh) => bodyTransform.GetComponent<MeshFilter>().mesh = mesh);
            unitAnimation = new V_UnitAnimation(unitSkeleton);
            unitAnimation.PlayAnim(GameAssets.UnitAnimTypeEnum.dMinion_Idle, 1f, null, null, null);

            state = State.Normal;
        }

        private void Update() {
            switch (state) {
            case State.Normal:
                if (playerHandler.IsDead()) {
                    unitAnimation.PlayAnim(GameAssets.UnitAnimTypeEnum.dMinion_Idle, lastMoveDir, 1f, null, null, null);
                } else {
                    HandleMovement();
                    HandleAttack();
                }
                break;
            case State.Busy:
                break;
            }
            unitSkeleton.Update(Time.deltaTime);
        }

        private void HandleMovement() {
            Vector3 moveDir = (playerHandler.GetPosition() - GetPosition()).normalized;
            lastMoveDir = moveDir;
            bool isIdle = moveDir == Vector3.zero;
            if (isIdle) {
                unitAnimation.PlayAnim(GameAssets.UnitAnimTypeEnum.dMinion_Idle, lastMoveDir, 1f, null, null, null);
            } else {
                unitAnimation.PlayAnim(GameAssets.UnitAnimTypeEnum.dMinion_Walk, lastMoveDir, 1f, null, null, null);
            }
            transform.position = transform.position + moveDir * speed * Time.deltaTime;
        }

        private void HandleAttack() {
            float distanceToPlayer = Vector3.Distance(GetPosition(), playerHandler.GetPosition());
            if (distanceToPlayer < 10f) {
                // Attack
                SetStateBusy();
                unitAnimation.PlayAnim(GameAssets.UnitAnimTypeEnum.dMinion_Attack, lastMoveDir, 1f, (UnitAnim unitAnim) => {
                    // Attack animation complete
                    SetStateNormal();
                }, (string trigger) => { 
                    // Animation trigger Damage player
                    playerHandler.GetHealthSystem().Damage(10);
                }, null);
            }
        }

        private void SetStateBusy() {
            state = State.Busy;
        }

        private void SetStateNormal() {
            state = State.Normal;
        }
        
        public Vector3 GetPosition() {
            return transform.position;
        }
        
        public HealthSystem GetHealthSystem() {
            return healthSystem;
        }
        
        private void HealthSystem_OnDead(object sender, EventArgs e) {
            // Dead! Destroy self
            if (OnDead != null) OnDead(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }

}