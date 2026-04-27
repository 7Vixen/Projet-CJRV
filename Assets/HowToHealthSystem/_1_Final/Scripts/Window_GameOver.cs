using UnityEngine;
using CodeMonkey.Utils;

namespace CodeMoney_HowToHealthSystem_1_Final {

    public class Window_GameOver : MonoBehaviour {

        private static Window_GameOver instance;

        private void Awake() {
            instance = this;
            gameObject.SetActive(false);
        }

        void Start() {
            transform.Find("RestartBtn").GetComponent<Button_UI>().ClickFunc = GameHandler.Restart;
        }

        public static void Show() {
            instance.gameObject.SetActive(true);
        }

    }

}