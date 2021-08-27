using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class ItemPickUp : Interactable
    {
        public WeaponItem weaponItem;

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            PickUpItem(playerManager);
            //Pick up item and add it to inventory
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;
            EquipSlotsUI equipSlotsUI;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();
            equipSlotsUI = FindObjectOfType<EquipSlotsUI>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; //halts player movement when collecting items
            animatorHandler.PlayTargetAnimation("Pick Up Item", true); //plays character designated pickup animation
            
            for (int i = 0; i < playerInventory.weaponsInPockets.Length; i++)
            {
                if (playerInventory.weaponsInPockets[i] == null)
                {
                    Debug.Log("assigning the weapon");
                    playerInventory.weaponsInPockets[i] = weaponItem;
                    equipSlotsUI.UpdateWeaponSlotsUI(weaponItem);
                    i = playerInventory.weaponsInPockets.Length;
                    Destroy(gameObject);
                }
            }            
        }
    }
}
