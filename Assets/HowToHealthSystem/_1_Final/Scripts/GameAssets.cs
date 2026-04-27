using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using V_AnimationSystem;

namespace CodeMoney_HowToHealthSystem_1_Final {

    public class GameAssets : MonoBehaviour {

        public static GameAssets i;
        
        private void Awake() {
            i = this;
        }


        public Transform pfPlayerTransform;
        public Transform pfEnemyTransform;
        public Transform pfHealthBar;
        public Transform pfSwordSlash;

        


        public static class UnitAnimTypeEnum {

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
            
            public static UnitAnimType dMinion_Idle;
            public static UnitAnimType dMinion_Walk;
            public static UnitAnimType dMinion_Attack;

        }

    }

}