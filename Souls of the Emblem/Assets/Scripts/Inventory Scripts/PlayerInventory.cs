using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class PlayerInventory : MonoBehaviour
    {
        WeaponSlotManager WeaponSlotManager;

        public WeaponItem rightWeapon;
        public WeaponItem leftWeapon;

        public WeaponItem unarmedWeapon;

        public WeaponItem[] weaponsInPockets = new WeaponItem[1]; //this is where we store weapons

        public int currentWeaponIndex = 0;//which of my weapons is equipped?

        private void Awake()
        {
            WeaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        }

        private void Start()
        {
            WeaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
            rightWeapon = weaponsInPockets[currentWeaponIndex];
            WeaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        }

        public void ChangeWeapon(int i)
        {
            currentWeaponIndex = currentWeaponIndex + i;

            if (currentWeaponIndex < -1)
            {
                currentWeaponIndex = weaponsInPockets.Length - 1;
            }

            if (currentWeaponIndex == 0 && weaponsInPockets[0] != null)
            {
                rightWeapon = weaponsInPockets[currentWeaponIndex];
                WeaponSlotManager.LoadWeaponOnSlot(weaponsInPockets[currentWeaponIndex], false);
            }
            else if (currentWeaponIndex == 0 && weaponsInPockets[0] == null)
            {
                currentWeaponIndex = currentWeaponIndex + i;
            }
            else if (currentWeaponIndex == 1 && weaponsInPockets[1] !=null)
            {
                rightWeapon = weaponsInPockets[currentWeaponIndex];
                WeaponSlotManager.LoadWeaponOnSlot(weaponsInPockets[currentWeaponIndex], false);
            }
            else if (currentWeaponIndex == 0 && weaponsInPockets[0] == null)
            {
                currentWeaponIndex = currentWeaponIndex + i;
            }
            else
            {
                currentWeaponIndex = -1;
                rightWeapon = unarmedWeapon; //unarmedWeapon
                WeaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }

            if (currentWeaponIndex > weaponsInPockets.Length - 1)
            {
                currentWeaponIndex = -1;
                rightWeapon = unarmedWeapon; //unarmedWeapon
                WeaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
        }
    }
}
