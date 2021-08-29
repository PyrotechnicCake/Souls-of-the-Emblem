using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pyro
{
    public class InputHandler : MonoBehaviour
    {
        //Initialize input values
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        //Inputs
        public bool dodgeInput;
        public bool attackInput;
        public bool heavyInput;
        public bool interactInput;
        public bool jumpInput;

        //depad inputs
        public bool d_Pad_Left;
        public bool d_Pad_Right;

        public bool RollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public float rollInputTimer;

        //track inputs
        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
        }

        //start reciving inputs
        public void OnEnable()
        {
            if (inputActions == null) //if no input actions are found, get them
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>(); //must be "inputActions" not "inputAction"
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        //stop recieving inputs
        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotInput();
            HandleInteractableButtonInput();
            HandleJumpInput();
        }

        //take movement inputs
        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta)
        {
            dodgeInput = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;

            if (dodgeInput)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    RollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.Attack.performed += i => attackInput = true;
            inputActions.PlayerActions.Heavy.performed += i => heavyInput = true;

            //R1 (attack button) is for attacks/staff effects
            if(attackInput) 
            {
                if(playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else if (!playerManager.isInteracting)
                {
                    if (playerManager.isInteracting)
                        return;
                    if (playerManager.canDoCombo)
                        return;

                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }
            }

            if (heavyInput && !playerManager.isInteracting)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }

        private void HandleQuickSlotInput()
        {
            inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
            inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true;
            if (d_Pad_Right)
            {
                playerInventory.ChangeWeapon(1);
            }
            else if(d_Pad_Left)
            {
                playerInventory.ChangeWeapon(-1);
            }
        }

        private void HandleInteractableButtonInput()
        {
            inputActions.PlayerActions.Interact.performed += i => interactInput = true;
        }

        private void HandleJumpInput()
        {
            inputActions.PlayerActions.Jump.performed += i => jumpInput = true;
        }
    }
}
