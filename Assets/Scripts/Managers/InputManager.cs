using SSN.Input;

using UnityEngine;

namespace SSN.InputSystem
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
		public GameControls Controls;

		#region Starting Lifecycle Functions
		private void Awake()
		{
			if(Instance != null && Instance != this)
			{
				Destroy(this);
				return;
			}

			Instance = this;
			Controls = new();
		}
		#endregion


		#region Enable Functions
		private void OnEnable()
		{
			Controls.Enable();
		}

		private void OnDisable()
		{
			Controls.Disable();
		}
		#endregion
	}
}
