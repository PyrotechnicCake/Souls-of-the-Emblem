using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class PlayerStats : MonoBehaviour
    {
        //basestats
        [Header("Base Stats")]
        public int baseHP = 28;
        public int baseStr = 13;
        public int baseMag = 2;
        public int baseSkl = 12;
        public int baseSpd = 10;
        public int baseLck = 6;
        public int baseDef = 14;
        public int baseRes = 3;

        //stats
        [Header("Stats")]
        public int maxHP;
        public int currentHP;
        public int str;
        public int mag;
        public int skl;
        public int spd;
        public int lck;
        public int def;
        public int res;

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
            str = baseStr;
            def = baseDef;
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
