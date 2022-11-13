using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
    //This controller script is based on 'AdvancedWalkerController' and limits player movement to a 2D plane;
    //It can be used to build 2D platformers or other games using 2D controls;
    public class SidescrollerController : AdvancedWalkerController
    {
        //Jump key variables;
        private int _jumpCounter = 0;
        private int _maxJumps = 2;

        //Calculate movement direction based on player input;
        protected override Vector3 CalculateMovementDirection()
        {
            //If no character input script is attached to this object, return;
            if (characterInput == null)
                return Vector3.zero;

            Vector3 _velocity = Vector3.zero;

            //If no camera transform has been assigned, use the character's 'right' transform axis to calculate the movement direction;
            if (cameraTransform == null)
            {
                _velocity += tr.right * characterInput.GetHorizontalMovementInput();
            }
            else
            {
                //If a camera transform has been assigned, use the assigned transform's 'right' axis for movement direction;
                //Project movement direction so movement stays parallel to the ground;
                _velocity += Vector3.ProjectOnPlane(cameraTransform.right, tr.up).normalized *
                             characterInput.GetHorizontalMovementInput();
            }

            return _velocity;
        }

        //Returns 'true' if the player presses the jump key;
        protected override bool IsJumpKeyPressed()
        {
            //If no character input script is attached to this object, return;
            if (characterInput == null)
                return false;

            if (mover.IsGrounded())
            {
                _jumpCounter = 0;
            }

            if (characterInput.IsJumpKeyPressed() && _jumpCounter < _maxJumps)
            {
                _jumpCounter++;
                return characterInput.IsJumpKeyPressed();
            }

            Debug.Log("_jumpCounter " + _jumpCounter);
            Debug.Log("grounded ?  " + mover.IsGrounded());

            return false;
        }
    }
}