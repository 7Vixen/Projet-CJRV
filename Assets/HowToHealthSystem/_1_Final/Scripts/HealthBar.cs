using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeMoney_HowToHealthSystem_1_Final {

    public class HealthBar : MonoBehaviour {

        private HealthSystem healthSystem;

        public void Setup(HealthSystem healthSystem) {
            this.healthSystem = healthSystem;

            healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
            UpdateHealthBar();
        }

        private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
            UpdateHealthBar();
        }
        private void UpdateHealthBar() {
            transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
        }

    }

}