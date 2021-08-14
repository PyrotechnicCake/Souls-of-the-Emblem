using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pyro
{
    public class EquipSlotsUI : MonoBehaviour
    {
        //find our inventory
        public PlayerInventory playerInventory;

        //Equipment slots
        public Image[] ESlot;

        //Equipment Icons
        public Image[] EIcon;

        //Equipment Names
        public Text[] EText;

        //selected slot
        public Image selectedSlot;

        int delta = 0;
        int beta = 0;

        public void UpdateWeaponSlotsUI(WeaponItem weapon)
        {
            
            //get the icons for the items in our inventory
            if (delta < EIcon.Length)
            {
                foreach (Image i in EIcon)
                {
                    print(delta);
                    if (playerInventory.weaponsInPockets[delta] != null)
                    {
                        i.enabled = true;
                        i.sprite = playerInventory.weaponsInPockets[delta].itemIcon;
                    }
                    else
                    {
                        i.sprite = null;
                        i.enabled = false;
                    }

                    delta += 1;
                }
            }

            //get the names for the items in our inventory
            if (beta < EText.Length)
            {
                foreach (Text i in EText)
                {
                    print(beta);
                    if (playerInventory.weaponsInPockets[beta] != null)
                    {
                        i.enabled = true;
                        i.text = playerInventory.weaponsInPockets[beta].itemName;
                    }
                    else
                    {
                        i.text = null;
                        i.enabled = false;
                    }

                    beta += 1;
                }
            }

            //lengthen current weapon slot
            //tack what weapon is equiped
            if ((playerInventory.currentWeaponIndex <= ESlot.Length - 1) && (playerInventory.currentWeaponIndex >= 0))
            {
                selectedSlot = ESlot[playerInventory.currentWeaponIndex];
            }
            else
            {
                selectedSlot = null;
            }

            //reset colour and size for non equipped weapons
            foreach (Image i in ESlot)                
            {
                i.rectTransform.sizeDelta = new Vector2(200, 35);
                i.color = new Color(0.85f, 0.81f, 0.8f);
            }

            if (selectedSlot != null)
            {
                selectedSlot.rectTransform.sizeDelta = new Vector2(215, 35);
                selectedSlot.color = new Color(0.95f, 0.91f, 0.9f);
            }
        }
    }
}
