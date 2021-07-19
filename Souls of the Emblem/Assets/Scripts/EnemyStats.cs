using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class EnemyStats : MonoBehaviour
    {
        public int baseHP = 28;
        public int maxHP;
        public int currentHP;

        //public HealthBar healthBar;

        Animator animator;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            maxHP = baseHP;
            currentHP = maxHP;
            //healthBar.SetMaxHealth(maxHP);
        }

        public void TakeDamage(int damage)
        {
            currentHP = currentHP - damage;

            //healthBar.SetCurrentHealth(currentHP);

            animator.Play("Damage_01");

            if(currentHP <= 0)
            {
                currentHP = 0;
                animator.Play("Death");
                //Handle dead player
            }
        }
    }
}
