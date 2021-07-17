using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class PlayerStats : MonoBehaviour
    {
        public int baseHP = 28;
        public int maxHP;
        public int currentHP;

        public HealthBar healthBar;

        AnimatorHandler animatorHandler;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        void Start()
        {
            maxHP = baseHP;
            currentHP = maxHP;
            healthBar.SetMaxHealth(maxHP);
        }

        public void TakeDamage(int damage)
        {
            currentHP = currentHP - damage;

            healthBar.SetCurrentHealth(currentHP);

            animatorHandler.PlayTargetAnimation("Damage_01", true);

            if(currentHP <= 0)
            {
                currentHP = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                //Handle dead player
            }
        }
    }
}
