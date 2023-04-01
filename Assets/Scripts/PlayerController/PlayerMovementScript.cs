using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerControllerCustom{
    public class PlayerMovementScript : MonoBehaviour
    {
        //Rigidbody playerRigidbody;

        public float decelerationSpeed;
        public float accelerationSpeed;
        public float maxZSpeed;
        public float minZSpeed;
        public float maxXSpeed;
        public float minXSpeed;
        public float playerZDirection;
        public float playerXDirection;

        public float playerZSpeed;
        public float playerXSpeed;
        // Start is called before the first frame update
        void Start()
        {
            //playerRigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            playerZDirection = Input.GetAxis("Vertical");
            playerXDirection = Input.GetAxis("Horizontal");
            
            //playerRigidbody.velocity = new Vector3(XPlayerMovement(playerXDirection), 0, ZPlayerMovement(playerZDirection));
            transform.Translate(new Vector3(XPlayerMovement(playerXDirection), 0, ZPlayerMovement(playerZDirection)));
        }

        public float ZPlayerMovement(float zDirection)
        {
            if(zDirection > 0.0f)
            {
                if(playerZSpeed >= 0.0f)
                {
                    if(playerZSpeed < maxZSpeed){
                        playerZSpeed = playerZSpeed + (accelerationSpeed * Time.deltaTime);

                        if(playerZSpeed > maxZSpeed)
                            playerZSpeed = maxZSpeed;
                    }
                }
                else if(playerZSpeed < 0.0f)
                {
                    playerZSpeed = playerZSpeed + ((decelerationSpeed * 2) * Time.deltaTime);
                }
            }
            else if(zDirection < 0.0f)
            {
                if(playerZSpeed <= 0.0f)
                {
                    if(playerZSpeed > minZSpeed){
                        playerZSpeed = playerZSpeed - (accelerationSpeed * Time.deltaTime);

                        if(playerZSpeed < minZSpeed)
                            playerZSpeed = minZSpeed;
                    }
                }
                else if(playerZSpeed > 0.0f)
                {
                    playerZSpeed = playerZSpeed - ((decelerationSpeed * 2) * Time.deltaTime);
                }
            }
            else if(zDirection == 0.0f)
            {
                if(playerZSpeed > 0.0f)
                {
                    playerZSpeed = playerZSpeed - (decelerationSpeed * Time.deltaTime);

                    if(playerZSpeed < 0.0f)
                        playerZSpeed = 0.0f;
                }
                else if(playerZSpeed < 0.0f)
                {
                    playerZSpeed = playerZSpeed + (decelerationSpeed * Time.deltaTime);

                    if(playerZSpeed > 0.0f)
                        playerZSpeed = 0.0f;
                }
            }

            return playerZSpeed;
        }

        public float XPlayerMovement(float xDirection)
        {
            if(xDirection > 0.0f)
            {
                if(playerXSpeed >= 0.0f)
                {
                    if(playerXSpeed < maxXSpeed){
                        playerXSpeed = playerXSpeed + (accelerationSpeed * Time.deltaTime);

                        if(playerXSpeed > maxXSpeed)
                            playerXSpeed = maxXSpeed;
                    }
                }
                else if(playerXSpeed < 0.0f)
                {
                    playerXSpeed = playerXSpeed + ((decelerationSpeed * 2) * Time.deltaTime);
                }
            }
            else if(xDirection < 0.0f)
            {
                if(playerXSpeed <= 0.0f)
                {
                    if(playerXSpeed > minXSpeed){
                        playerXSpeed = playerXSpeed - (accelerationSpeed * Time.deltaTime);

                        if(playerXSpeed < minXSpeed)
                            playerXSpeed = minXSpeed;
                    }
                }
                else if(playerXSpeed > 0.0f)
                {
                    playerXSpeed = playerXSpeed - ((decelerationSpeed * 2) * Time.deltaTime);
                }
            }
            else if(xDirection == 0.0f)
            {
                if(playerXSpeed > 0.0f)
                {
                    playerXSpeed = playerXSpeed - (decelerationSpeed * Time.deltaTime);

                    if(playerXSpeed < 0.0f)
                        playerXSpeed = 0.0f;
                }
                else if(playerXSpeed < 0.0f)
                {
                    playerXSpeed = playerXSpeed + (decelerationSpeed * Time.deltaTime);

                    if(playerXSpeed > 0.0f)
                        playerXSpeed = 0.0f;
                }
            }

            return playerXSpeed;
        }
    }
}