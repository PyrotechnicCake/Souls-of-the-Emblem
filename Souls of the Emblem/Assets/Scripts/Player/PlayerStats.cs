using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class PlayerStats : MonoBehaviour
    {
        //basestats
        [Header("Base Stats")]
        public int baseHP = 1;
        public int baseStr = 1;
        public int baseMag = 2;
        public int baseSkl = 12;
        public int baseSpd = 1;
        public int baseLck = 6;
        public int baseDef = 1;
        public int baseRes = 3;

        //stats
        [Header("Stats")]
        public int maxHP;
        public int currentHP;
        public int maxStam;
        public int currentStam;
        public int str;
        public int mag;
        public int skl;
        public int spd;
        public int lck;
        public int def;
        public int res;

        public HealthBar healthBar;
        public StaminaBar stamBar;

        AnimatorHandler animatorHandler;
        CharacterStats characterStats;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        void Start()
        {
            //find my character
            characterStats = GetComponentInChildren<CharacterStats>();

            //set up the hp bar
            maxHP = characterStats.baseHP;
            currentHP = maxHP;            
            healthBar.SetMaxHealth(maxHP);
            
            //set up the stats
            str = characterStats.baseStr;
            spd = characterStats.baseSpd;
            def = characterStats.baseDef;

            //set up the stamina bar (THIS HAS TO BE AFTER YOU SET SPEED)
            maxStam = SetMaxStaminaFromSpeed();
            currentStam = maxStam;
            stamBar.SetMaxStam(maxStam);
        }

        private int SetMaxStaminaFromSpeed()
        {
            maxStam = ((Mathf.RoundToInt(spd/3)) + 4);
            return maxStam;
        }

        public void TakeDamage(int damage)
        {
            if (damage - def > 0)
            {
                currentHP = currentHP - (damage - def);
            }

            healthBar.SetCurrentHealth(currentHP);

            animatorHandler.PlayTargetAnimation("Damage_01", true);

            if(currentHP <= 0)
            {
                currentHP = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                //Handle dead player
            }
        }

        public void TakeStaminaDamage(int damage)
        {
            currentStam = currentStam - damage;
            stamBar.SetCurrentStam(currentStam);
        }
    }
}
