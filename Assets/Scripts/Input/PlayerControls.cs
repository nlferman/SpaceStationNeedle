using SSN.Character.Physics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SSN.Input
{
    public class PlayerControls : MonoBehaviour
    {
        private CharacterController2D CharacterController;
        private GameControls Controls;

		private void Awake()
		{
			//The below will be updated when I create the input manager.
			Controls = new();
			CharacterController = GetComponent<CharacterController2D>();
		}

		public void OnMovePerformed(InputAction.CallbackContext _ctx)
		{
			Vector2 _move = _ctx.ReadValue<Vector2>();
			CharacterController.ChangeDirection(_move);
		}

		public void OnMoveCancelled(InputAction.CallbackContext _ctx)
		{
			CharacterController.ChangeDirection(Vector2.zero);
		}

		private void OnEnable()
		{
			if(Controls is null) return;

			Controls.Enable();
			EnablePlayerControls();
		}

		private void EnablePlayerControls()
		{
			Controls.Player.Move.performed += OnMovePerformed;
			Controls.Player.Move.canceled += OnMoveCancelled;
		}

		private void OnDisable()
		{
			if(Controls is null) return;

			DisablePlayerControls();
		}

		private void DisablePlayerControls()
		{
			Controls.Player.Move.performed -= OnMovePerformed;
			Controls.Player.Move.canceled -= OnMoveCancelled;
		}
	}
}