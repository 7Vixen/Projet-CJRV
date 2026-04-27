using System;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

namespace CodeMoney_HowToHealthSystem_1_Final {

    public class GameHandler : MonoBehaviour {
        
        private PlayerHandler playerHandler;
        private List<EnemyHandler> enemyHandlerList;

        [SerializeField]
        private CameraFollow cameraFollow;

        private void Start() {
            enemyHandlerList = new List<EnemyHandler>();
            
            playerHandler = PlayerHandler.CreatePlayer(GetClosestEnemyHandler);
            playerHandler.OnDead += delegate (object sender, EventArgs e) {
                Window_GameOver.Show();
            };
            
            cameraFollow.Setup(70f, 2f, playerHandler.GetPosition);
            
            FunctionPeriodic.Create(SpawnEnemy, 1.5f);
        }

        private void SpawnEnemy() {
            Vector3 spawnPosition = playerHandler.GetPosition() + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(50, 100f);
            EnemyHandler enemyHandler = EnemyHandler.CreateEnemy(spawnPosition, playerHandler);
            enemyHandler.OnDead += EnemyHandler_OnDead;
            enemyHandlerList.Add(enemyHandler);
        }

        private void EnemyHandler_OnDead(object sender, System.EventArgs e) {
            EnemyHandler enemyHandler = sender as EnemyHandler;
            enemyHandlerList.Remove(enemyHandler);
        }

        private EnemyHandler GetClosestEnemyHandler(Vector3 playerPosition) {
            const float maxDistance = 50f;
            EnemyHandler closest = null;
            foreach (EnemyHandler enemyHandler in enemyHandlerList) {
                if (Vector3.Distance(playerPosition, enemyHandler.GetPosition()) > maxDistance) continue;
                if (closest == null) {
                    closest = enemyHandler;
                } else {
                    if (Vector3.Distance(playerPosition, enemyHandler.GetPosition()) < Vector3.Distance(playerPosition, closest.GetPosition())) {
                        closest = enemyHandler;
                    }
                }
            }
            return closest;
        }

        public static void Restart() {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

    }

}