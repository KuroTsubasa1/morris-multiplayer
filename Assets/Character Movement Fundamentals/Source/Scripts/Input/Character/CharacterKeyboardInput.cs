﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMF
{
	//This character movement input class is an example of how to get input from a keyboard to control the character;
    public class CharacterKeyboardInput : CharacterInput
    {
		public string horizontalInputAxis = "Horizontal";
		public string verticalInputAxis = "Vertical";
		public KeyCode jumpKey = KeyCode.Space;
		public bool hasDoubleJump = false;
		
		//If this is enabled, Unity's internal input smoothing is bypassed;
		public bool useRawInput = true;

        public override float GetHorizontalMovementInput()
		{
			if(useRawInput)
				return Input.GetAxisRaw(horizontalInputAxis);
			else
				return Input.GetAxis(horizontalInputAxis);
		}

		public override float GetVerticalMovementInput()
		{
			return 0;
		}

		public override bool IsJumpKeyPressed()
		{
			return Input.GetKey(jumpKey);
		}
		
		
    }
}
