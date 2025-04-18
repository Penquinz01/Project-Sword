using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets_InputSystem
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		private StarterAssets_Input _playerInput;
        [Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interaction;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		public InputActionMap map;

		[HideInInspector]
		public bool unMounted = false;
		private InputActionMap _playerMap;
		private InputActionMap _horseMap;

		public String currentAction;

#if ENABLE_INPUT_SYSTEM
		public void SwitchActionMap(String s)
		{
			if (s.Equals("Horse", StringComparison.OrdinalIgnoreCase))
			{
				_playerMap.Disable();
				_horseMap.Enable();
				currentAction = _horseMap.name;
            }
			else if (s.Equals("Player", StringComparison.OrdinalIgnoreCase))
			{
				_horseMap.Disable();
				_playerMap.Enable();
                currentAction = _playerMap.name;
            }
			else
			{
				Debug.LogError("Invalid action map name");
			}
		}
        private void Awake()
        {
            _playerInput = new StarterAssets_Input();
            _playerInput.Enable();
			_playerInput.Player.Enable();
            _playerInput.Player.Move.performed += ctx => MoveInput(ctx.ReadValue<Vector2>());
            _playerInput.Player.Move.canceled += ctx => MoveInput(Vector2.zero);
            _playerInput.Player.Look.performed += ctx => LookInput(ctx.ReadValue<Vector2>());
            _playerInput.Player.Look.canceled += ctx => LookInput(Vector2.zero);
            _playerInput.Player.Jump.started += ctx => JumpInput(ctx.ReadValueAsButton());
			_playerInput.Player.Sprint.started += ctx => SprintInput(ctx.ReadValueAsButton());
			_playerInput.Player.Interact.started += ctx => InteractionInput(ctx.ReadValueAsButton());
			_playerInput.Horse.Move.performed += ctx => MoveInput(ctx.ReadValue<Vector2>());
            _playerInput.Horse.Move.canceled += ctx => MoveInput(Vector2.zero);
            _playerInput.Horse.Look.performed += ctx => LookInput(ctx.ReadValue<Vector2>());
            _playerInput.Horse.Look.canceled += ctx => LookInput(Vector2.zero);
			_playerInput.Horse.Jump.started += ctx => JumpInput(ctx.ReadValueAsButton());
			_playerInput.Horse.Sprint.started += ctx => SprintInput(ctx.ReadValueAsButton());
			_playerInput.Horse.Unmount.started += ctx => unMounted = true;
			_playerMap = _playerInput.Player;
            _horseMap = _playerInput.Horse;
			currentAction = _playerMap.name;
        }
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
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
		public void OnInteraction(InputValue value)
        {
			Debug.Log("E pressed");
            InteractionInput(value.isPressed);
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
        private void InteractionInput(bool isPressed)
        {
            interaction = isPressed;
        }
    }
	
}