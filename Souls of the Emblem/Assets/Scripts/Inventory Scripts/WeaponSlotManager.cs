using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class WeaponSlotManager : MonoBehaviour
    {
        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;

        //shield collider
        DamageCollider weaponCollider;

        public WeaponItem attackingWeapon;

        Animator animator;

        EquipSlotsUI equipSlotsUI;

        PlayerStats playerStats;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            equipSlotsUI = FindObjectOfType<EquipSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
            {
                if(weaponSlot.isLeftHandSlot)
                {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot)
                {
                    rightHandSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
        {
            if(isLeft)
            {
                leftHandSlot.LoadWeaponModel(weaponItem);
                //loadshieldcollider
            }
            else
            {
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadWeaponDamageCollider();
                equipSlotsUI.UpdateWeaponSlotsUI(weaponItem);

                if (weaponItem != null)
                {
                    animator.CrossFade(weaponItem.weapon_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Locomotion", 0.2f);
                }
            }
        }

        #region Handle Weapon Damage Colliders

        public void LoadWeaponDamageCollider()
        {
            weaponCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

        public void OpenWeaponDamageCollider()
        {
            weaponCollider.EnableDamageCollider();
        }

        public void CloseWeaponDamageCollider()
        {
            weaponCollider.DisableDamageCollider();
        }

        #endregion

        public void DrainStaminaLight()
        {
            float stamMultiplier;

            stamMultiplier = attackingWeapon.LightAttackMultiplier;

            WeaponStats weaponStats = GetComponentInChildren<WeaponStats>();
            playerStats.TakeStaminaDamage(Mathf.RoundToInt((weaponStats.Wt - (playerStats.str / 5)) * stamMultiplier));
        }
        public void DrainStaminaHeavy()
        {
            float stamMultiplier;

            stamMultiplier = attackingWeapon.HeavyAttackMultiplier;

            WeaponStats weaponStats = GetComponentInChildren<WeaponStats>();
            playerStats.TakeStaminaDamage(Mathf.RoundToInt((weaponStats.Wt - (playerStats.str / 5)) * stamMultiplier));
        }
    }
}
