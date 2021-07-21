﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Idle animations")]
        public string weapon_idle;

        [Header("Attack animations")]
        public string Light_Attack_1;
        public string Light_Attack_2;
        public string Heavy_Attack_1;
    }
}
