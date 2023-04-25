using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


    public class PlayerMovementScript : MonoBehaviour
    {
        Rigidbody playerRigidbody;
        CapsuleCollider playerCollider;
        CapsuleCollider jumpCollider;
        float colliderScaleX, colliderScaleY, colliderScaleZ;
        public LayerMask GroundLayer;
        public LayerMask wallLayer;

        public float accelerationSpeed;
        public float maxXSpeed;
        public float ZSpeedModifier;
        public float backwardSpeedModifier;
        public float airMultiplier;
        public float jumpAmount;
        public float groundDrag;
        
        private float velocityDirection;
        private float playerZDirection;
        private float playerXDirection;
        private float currentMaxSpeed;
        private float distToGround;
        private bool canMove;
        private bool jumpInput;
        private bool playerIsGrounded;
        private bool playerIsTouchingWall;
        private Vector3 moveDirection;
        private Vector3 movementVector;

        ////    For old movement; obsolete    ////
        //public float maxZSpeed;
        //public float minZSpeed;
        //public float decelerationSpeed;
        //public float minXSpeed;

        //private float playerZSpeed;
        //private float playerXSpeed;

        
        
        // Start is called before the first frame update
        void Start()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            playerCollider = GetComponent<CapsuleCollider>();
            jumpCollider = GetComponentInChildren<CapsuleCollider>();
            distToGround = jumpCollider.bounds.extents.y;

            canMove = true;

            colliderScaleX = transform.localScale.x;
            colliderScaleY = transform.localScale.y;
            colliderScaleZ = transform.localScale.z;
        }

        // Update is called once per frame
        void Update()
        {
            if(canMove){
                playerZDirection = Input.GetAxisRaw("Vertical");
                playerXDirection = Input.GetAxisRaw("Horizontal");
                playerIsGrounded = IsGrounded();
                playerIsTouchingWall = IsTouchingWall();
                jumpInput = Input.GetKeyDown(KeyCode.Space);

                //change drag
                if (playerIsGrounded)
                    playerRigidbody.drag = groundDrag;
                else
                    playerRigidbody.drag = 0;
                
                //Jump
                if (jumpInput)
                {
                    if(playerIsGrounded)
                    {
                        playerRigidbody.AddForce(Vector3.up * jumpAmount);
                    }

                }

                limitSpeed();
            }
        }

        void FixedUpdate()
        {
            if(canMove)
            {
                MovePlayer();

                ////    Tested solutions with old movement system    ////
                /*
                if(playerIsGrounded)
                {
                    movementVector = new Vector3(XPlayerMovement(playerXDirection) * Time.deltaTime,
                                                0f,
                                                ZPlayerMovement(playerZDirection)* Time.deltaTime);

                    playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);
                }
                */
                //transform.Translate(movementVector);

                //playerRigidbody.velocity = transform.TransformDirection(movementVector);
                //playerRigidbody.AddForce(Physics.gravity,ForceMode.Acceleration);

                //playerRigidbody.AddForce(movementVector, ForceMode.Force);

            }
        }


        ////    Old movement system         ////
        ////    A.K.A                       ////
        ////    Sorrow and Broken Dreams    ////
        /*
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
        */

        public void LockMovement()
        {
            canMove = false;
        }

        public void UnlockMovement()
        {
            canMove = true;
        }

        private void MovePlayer()
        {
            // calculate movement direction
            moveDirection = (transform.forward * playerZDirection) + (transform.right * playerXDirection);

            if(playerIsGrounded)
                playerRigidbody.AddForce(moveDirection.normalized * accelerationSpeed, ForceMode.Force);
            else {
                playerRigidbody.AddForce(moveDirection.normalized * accelerationSpeed * airMultiplier, ForceMode.Force);
            }
        }

        public void limitSpeed()
        {
            velocityDirection = Vector3.Dot(transform.forward, playerRigidbody.velocity.normalized);
            
            float appliedBackMod = 0f;
            if(velocityDirection < 0f)
                appliedBackMod = backwardSpeedModifier * Mathf.Abs(velocityDirection);

            currentMaxSpeed = maxXSpeed + (ZSpeedModifier * Mathf.Abs(velocityDirection)) - appliedBackMod;

            movementVector = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

            if(movementVector.magnitude > (currentMaxSpeed))
            {
                Vector3 limitedVelocity = movementVector.normalized * currentMaxSpeed;
                playerRigidbody.velocity = new Vector3 (limitedVelocity.x, playerRigidbody.velocity.y, limitedVelocity.z);
            }
        }

        public bool IsGrounded()
        {
            return Physics.CheckCapsule(jumpCollider.bounds.center,
            new Vector3(jumpCollider.bounds.center.x,jumpCollider.bounds.min.y + 0.25f,jumpCollider.bounds.center.z),
            jumpCollider.radius, GroundLayer);
        }

        public bool IsTouchingWall()
        {
            return Physics.CheckCapsule(jumpCollider.bounds.center,
            new Vector3(jumpCollider.bounds.center.x,jumpCollider.bounds.min.y + 0.25f,jumpCollider.bounds.center.z),
            jumpCollider.radius, wallLayer);
        }

    }
