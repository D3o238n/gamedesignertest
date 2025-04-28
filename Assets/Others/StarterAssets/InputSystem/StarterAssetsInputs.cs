using UnityEngine;
using Test;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;

#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		bool Control_bool = true;

        private void Start()
        {
			Game_administrator.Singleton.Player_control_event.AddListener(Change_control); 
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
        }

		public void OnLook(InputValue value)
		{
                if (cursorInputForLook)
			{
                    LookInput(value.Get<Vector2>());
            }

		}

		public void OnJump(InputValue value)
		{
            if (Control_bool)
                JumpInput(value.isPressed);
			else
                JumpInput(false);
        }

		public void OnSprint(InputValue value)
		{
            if (Control_bool)
                SprintInput(value.isPressed);
            else
                SprintInput(false);
        }
#endif

		void Change_control(bool _change)
		{
			Control_bool = _change;
			MoveInput(Vector2.zero);
            LookInput(Vector2.zero);
        }

		public void MoveInput(Vector2 newMoveDirection)
		{
            if (Control_bool)
                move = newMoveDirection;
            else
                move = Vector2.zero;
        } 

		public void LookInput(Vector2 newLookDirection)
		{
            if (Control_bool)
                look = newLookDirection;
            else
                look = Vector2.zero;
        }

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}