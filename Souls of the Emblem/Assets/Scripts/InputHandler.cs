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

        //track inputs
        PlayerControls inputActions;

        Vector2 movementInput;
        Vector2 cameraInput;

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
    }
}
