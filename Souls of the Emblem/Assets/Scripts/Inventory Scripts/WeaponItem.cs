using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        //Weapon Animations
        [Header("Idle animations")]
        public string weapon_idle;

        [Header("Attack animations")]
        public string Light_Attack_1;
        public string Light_Attack_2;
        public string Heavy_Attack_1;

        public int baseStamina; //we'll need to change this or maybe just delete it later
        public float LightAttackMultiplier = 1;
        public float HeavyAttackMultiplier = 2; //we can change these later per each weapon but this makes less work in the long run
    }
}
