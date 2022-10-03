using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : Singleton<StarterAssetsInputs>
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

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

        void Start()
        {
			Cursor.lockState = CursorLockMode.Locked;    
        }

        public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnInteract()
        {
            //print(OnInteractPressed.GetInvocationList().Length);

            //for (int i = 0; i < OnInteractPressed.GetInvocationList().Length; i++)
            //{
            //    print(OnInteractPressed.GetInvocationList()[i].Target.ToString());
            //}

            if (OnInteractPressed != null)
                OnInteractPressed.Invoke();
        }

		public void OnPause()
        {
			if (OnPauseMenuPressed != null)
			{
				OnPauseMenuPressed.Invoke();
			}

        }

#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
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

		public void DisableMouseLook()
        {
			cursorInputForLook = false;
		}

		public void EnableMouseLook()
        {
			cursorInputForLook = true;
        }


        public static Action OnInteractPressed;

		public static Action OnPauseMenuPressed;

		public void SubscribeToInteractInput(Action a)
        {
            OnInteractPressed += a;
        }

        public void UnsubscribeToInteractInput(Action a)
        {
            OnInteractPressed -= a;
        }
    }
	
}