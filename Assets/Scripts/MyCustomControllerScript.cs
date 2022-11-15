using System;
using UnityEngine;
using CMF;

namespace DefaultNamespace
{
    public class MyCustomControllerScript : Controller
    {
        private Mover mover;
        float currentVerticalSpeed = 0f;
        bool isGrounded;
        public float movementSpeed = 7f;
        public float jumpSpeed = 10f;
        public float gravity = 10f;

        private int _countJumps = 0;
        public int maxJumps = 2;

        //Jump key variables;
        bool jumpInputIsLocked;
        bool jumpKeyIsPressed;

        Vector3 lastVelocity = Vector3.zero;

        public Transform cameraTransform;
        CharacterInput characterInput;
        Transform tr;

        // Use this for initialization
        void Start()
        {
            tr = transform;
            mover = GetComponent<Mover>();
            characterInput = GetComponent<CharacterInput>();
        }

        protected virtual bool IsJumpKeyPressed()
        {
            //If no character input script is attached to this object, return;
            if (characterInput == null)
                return false;

            return characterInput.IsJumpKeyPressed();
        }

        void HandleJumpKeyInput()
        {
            bool newJumpKeyPressedState = IsJumpKeyPressed();
    
            /*
            if (jumpKeyIsPressed == false && _newJumpKeyPressedState)
                jumpKeyWasPressed = true;
            */

            if (jumpKeyIsPressed)
            {   
                // lock jump to simulate key down
                jumpInputIsLocked = true;
            }
            

            jumpKeyIsPressed = newJumpKeyPressedState;
            Debug.Log("Update: jumpKeyIsPressed " + jumpKeyIsPressed);
           
        }

        private void Update()
        {
            HandleJumpKeyInput();
        }

        void FixedUpdate()
        {
            //Run initial mover ground check;
            mover.CheckForGround();

            //If character was not grounded int the last frame and is now grounded, call 'OnGroundContactRegained' function;
            if (isGrounded == false && mover.IsGrounded() == true)
                OnGroundContactRegained(lastVelocity);

            //Check whether the character is grounded and store result;
            isGrounded = mover.IsGrounded();

            Vector3 _velocity = Vector3.zero;

            //Add player movement to velocity;
            _velocity += CalculateMovementDirection() * movementSpeed;

            //Handle gravity;
            if (!isGrounded)
            {
                currentVerticalSpeed -= gravity * Time.deltaTime;

                // if player starts falling enable second jump
                if (currentVerticalSpeed < 0)
                {   
                    jumpInputIsLocked = false;
                }
            }
            else
            {
                if (currentVerticalSpeed <= 0f)
                    // unlock jump when grounded
                    jumpInputIsLocked = false;
                    currentVerticalSpeed = 0f;
            }

            //Handle jumping;
            if (jumpKeyIsPressed &&  _countJumps < maxJumps && !jumpInputIsLocked)
            {
                OnJumpStart();
                currentVerticalSpeed = jumpSpeed;
                isGrounded = false;
                _countJumps++;
            }

            //Add vertical velocity;
            _velocity += tr.up * currentVerticalSpeed;

            //Save current velocity for next frame;
            lastVelocity = _velocity;

            mover.SetExtendSensorRange(isGrounded);
            mover.SetVelocity(_velocity);

            if (isGrounded)
            {
                _countJumps = 0;
                // unlock jump when grounded
                jumpInputIsLocked = false;
            }
        }

        private Vector3 CalculateMovementDirection()
        {
            //If no character input script is attached to this object, return no input;
            if (characterInput == null)
                return Vector3.zero;

            Vector3 _direction = Vector3.zero;

            //If no camera transform has been assigned, use the character's transform axes to calculate the movement direction;
            if (cameraTransform == null)
            {
                _direction += tr.right * characterInput.GetHorizontalMovementInput();
                _direction += tr.forward * characterInput.GetVerticalMovementInput();
            }
            else
            {
                //If a camera transform has been assigned, use the assigned transform's axes for movement direction;
                //Project movement direction so movement stays parallel to the ground;
                _direction += Vector3.ProjectOnPlane(cameraTransform.right, tr.up).normalized *
                              characterInput.GetHorizontalMovementInput();
                _direction += Vector3.ProjectOnPlane(cameraTransform.forward, tr.up).normalized *
                              characterInput.GetVerticalMovementInput();
            }

            //If necessary, clamp movement vector to magnitude of 1f;
            if (_direction.magnitude > 1f)
                _direction.Normalize();

            return _direction;
        }

        //This function is called when the controller has landed on a surface after being in the air;
        void OnGroundContactRegained(Vector3 _collisionVelocity)
        {
            //Call 'OnLand' delegate function;
            if (OnLand != null)
                OnLand(_collisionVelocity);
        }

        //This function is called when the controller has started a jump;
        void OnJumpStart()
        {
            //Call 'OnJump' delegate function;
            if (OnJump != null)
                OnJump(lastVelocity);
        }

        //Return the current velocity of the character;
        public override Vector3 GetVelocity()
        {
            return lastVelocity;
        }

        //Return only the current movement velocity (without any vertical velocity);
        public override Vector3 GetMovementVelocity()
        {
            return lastVelocity;
        }

        //Return whether the character is currently grounded;
        public override bool IsGrounded()
        {
            return isGrounded;
        }
    }
}