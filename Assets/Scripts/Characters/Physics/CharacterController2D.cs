using UnityEngine;

namespace SSN.Character.Physics
{
    using Core;
    using States;

    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class CharacterController2D : MonoBehaviour
    {
		#region Velocity
        public Vector2 Velocity = Vector2.zero;
        public Vector2 PreviousVelocity = Vector2.zero;
        public float Speed = 5f;
        public float RunMultiplier = 1.25f;
        #endregion

        #region Components
        protected Rigidbody2D RGB2D;
        protected BoxCollider2D Collider;
        protected Character2D Character2D;
		#endregion

		#region Starter Cycle Functions
        protected virtual void Awake()
        {
            
        }
		#endregion
	}
}
