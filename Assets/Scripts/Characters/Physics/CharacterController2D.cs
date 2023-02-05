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

        protected float CurrentVelocityMultiplier = 1f;
        public Vector2 Direction = Vector2.zero;

        #region Components
        protected Rigidbody2D RGB2D;
        protected BoxCollider2D Collider;
        protected Character2D Character2D;
		#endregion

		#region Starter Cycle Functions
        protected virtual void Awake()
        {
            Initalization();
        }
		protected virtual void Start()
		{

		}
        protected virtual void Initalization()
        {
            RGB2D = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
            Character2D = GetComponent<Character2D>();
        }
        protected virtual void ResetController()
        {
            RGB2D.freezeRotation = true;
        }
		#endregion

        protected virtual void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {

        }
        //More will be added here with different logic.
        public virtual void ChangeDirection(Vector2 _direction)
        {
            Direction = _direction;
        }
        protected virtual void AddForce(Vector2 _force, ForceMode2D _forceMode2D = ForceMode2D.Impulse)
        {

        }
        protected virtual void SetStates()
        {
            if(Velocity != Vector2.zero)
            {
                Character2D.CharacterMoveState = CharacterMoveState.Walking;
                Character2D.SetFacingDirection((Direction.x > 0) ? FacingDirection.Right : FacingDirection.Left, Direction);
            }
            else
            {
				Character2D.CharacterMoveState = CharacterMoveState.Idle;
                Character2D.SetFacingDirection((PreviousVelocity.x > 0) ? FacingDirection.Right : FacingDirection.Left, PreviousVelocity);
			}
        }
	}
}