using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class PlayerLocomotion : MonoBehaviour
    {
        //initialize objects and vectors
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        //track position
        [HideInInspector]
        public Transform myTransform;

        //initialize rigidbody and camera
        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;

        // Start is called before the first frame update
        void Start()
        {
            //get rigidbody, camera and input handler
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;

        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

            myTransform.rotation = targetRotation;
        }
        #endregion
    }
}
