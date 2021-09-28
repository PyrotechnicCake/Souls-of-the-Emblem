using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;

        public InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableUIGameObject;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;

        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            interactableUI = FindObjectOfType<InteractableUI>();
        }

        void Update()
        {
            anim.SetBool("isInAir", isInAir);
            isInteracting = anim.GetBool("isInteracting");

            float delta = Time.deltaTime;

            canDoCombo = anim.GetBool("canDoCombo");

            inputHandler.TickInput(delta);

            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);            
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
            playerLocomotion.HandleJumping();

            CheckForInteractableObject();

            Quaternion notRotate = playerLocomotion.myTransform.rotation; //the things I have to do to stop the thing from fucvkig toreateingh
            notRotate.x = 0;
            notRotate.z = 0;
            playerLocomotion.myTransform.rotation = notRotate;
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.RollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.attackInput = false;
            inputHandler.heavyInput = false;
            inputHandler.interactInput = false;
            inputHandler.jumpInput = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;

            if (isInAir)
            {
                playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
                isInteracting = true;
            }
        }

        public void CheckForInteractableObject()
        {
            RaycastHit hit;

            if(Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        //do random UI shit
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);


                    if(inputHandler.interactInput)
                        {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else
            {
                if (interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }

                if (itemInteractableUIGameObject != null && inputHandler.interactInput)
                {
                    itemInteractableUIGameObject.SetActive(false);
                }
            }
        }
    }
}
