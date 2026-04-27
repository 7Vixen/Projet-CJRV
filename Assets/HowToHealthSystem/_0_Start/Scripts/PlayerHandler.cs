using System.Reflection;
using UnityEngine;
using V_AnimationSystem;

/*
 * Player movement with WASD keys
 * Attack with Space
 * */
public class PlayerHandler : MonoBehaviour {

    private const float speed = 40f;
        
    private V_UnitSkeleton unitSkeleton;
    private V_UnitAnimation unitAnimation;
    private Vector3 lastMoveDir;
    private State state;

    private enum State {
        Normal,
        Busy,
    }

    private void Start() {
        Transform bodyTransform = transform.Find("Body");
        unitSkeleton = new V_UnitSkeleton(1f, bodyTransform.TransformPoint, (Mesh mesh) => bodyTransform.GetComponent<MeshFilter>().mesh = mesh);
        unitAnimation = new V_UnitAnimation(unitSkeleton);
        unitAnimation.PlayAnim(UnitAnimTypeEnum.dSwordTwoHandedBack_Idle, 1f, null, null, null);

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

        Vector3 moveDir = new Vector3(moveX, moveY);
        lastMoveDir = moveDir;
        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle) {
            unitAnimation.PlayAnim(UnitAnimTypeEnum.dSwordTwoHandedBack_Idle, lastMoveDir, 1f, null, null, null);
        } else {
            unitAnimation.PlayAnim(UnitAnimTypeEnum.dSwordTwoHandedBack_Walk, lastMoveDir, .75f, null, null, null);
        }
        transform.position = transform.position + moveDir * speed * Time.deltaTime;
    }
    private void HandleAttack() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Attack
            SetStateBusy();
            Vector3 attackDir = lastMoveDir;
            lastMoveDir = attackDir;
            transform.position = transform.position + lastMoveDir * 5f;
            UnitAnimType activeAnimType = unitAnimation.GetActiveAnimType();
            if (activeAnimType == UnitAnimTypeEnum.dSwordTwoHandedBack_Sword) {
                unitAnimation.PlayAnimForced(UnitAnimTypeEnum.dSwordTwoHandedBack_Sword2, lastMoveDir, 1f, (UnitAnim unitAnim) => SetStateNormal(), null, null);
            } else {
                unitAnimation.PlayAnimForced(UnitAnimTypeEnum.dSwordTwoHandedBack_Sword, lastMoveDir, 1f, (UnitAnim unitAnim) => SetStateNormal(), null, null);
            }
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


        

    private class UnitAnimTypeEnum {

        static UnitAnimTypeEnum() {
            FieldInfo[] fieldInfoArr = typeof(UnitAnimTypeEnum).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo fieldInfo in fieldInfoArr) {
                if (fieldInfo != null) {
                    fieldInfo.SetValue(null, UnitAnimType.GetUnitAnimType(fieldInfo.Name));
                }
            }
        }
            
        public static UnitAnimType dSwordTwoHandedBack_Idle;
        public static UnitAnimType dSwordTwoHandedBack_Walk;
        public static UnitAnimType dSwordTwoHandedBack_Sword;
        public static UnitAnimType dSwordTwoHandedBack_Sword2;

    }
}
