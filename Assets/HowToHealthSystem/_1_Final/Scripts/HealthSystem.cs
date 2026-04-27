using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeMoney_HowToHealthSystem_1_Final {

    public class HealthSystem {

        public event EventHandler OnHealthChanged;
        public event EventHandler OnDead;

        private int healthMax;
        private int health;

        public HealthSystem(int healthMax) {
            this.healthMax = healthMax;
            health = healthMax;
        }
        
        public float GetHealthPercent() {
            return (float)health / healthMax;
        }

        public void Damage(int amount) {
            health -= amount;
            if (health < 0) {
                health = 0;
            }
            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);

            if (health <= 0) {
                Die();
            }
        }

        public void Die() {
            if (OnDead != null) OnDead(this, EventArgs.Empty);
        }

        public void Heal(int amount) {
            health += amount;
            if (health > healthMax) {
                health = healthMax;
            }
            if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        }

    }

}