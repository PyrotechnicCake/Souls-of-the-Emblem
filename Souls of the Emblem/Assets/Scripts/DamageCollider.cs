using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;

        //attack damage value
        int attack;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
            //add my attack
            attack = GetComponent<WeaponStats>().Mt;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.tag == "Player")
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();

                if (playerStats != null)
                {
                    playerStats.TakeDamage(attack - playerStats.def);
                }
            }

            if (collision.tag == "Enemy")
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                PlayerStats playerStats = GetComponentInParent<PlayerStats>();
                int addedStr = 0;

                if (playerStats != null)
                {
                    addedStr = playerStats.str;
                }

                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(attack + addedStr);
                }
            }
        }
    }
}
