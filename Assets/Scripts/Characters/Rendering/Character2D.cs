using UnityEngine;

namespace SSN.Character.Core
{
    using States;
    using Physics;
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class Character2D : MonoBehaviour
    {
        public bool isPlayer = false;

		#region States
		[Header("States")]
        public CharacterMoveState CharacterMoveState = CharacterMoveState.Idle;
        public CharacterStatusState CharacterStatusState = CharacterStatusState.Alive;
        public CharacterControlState CharacterControlState;
        protected string stateString = "";
		#endregion

		#region Facing Directions
		[Header("Facing Directions")]
        public FacingDirection FacingDirection;
        public FacingDirection StartDirection;
        public FacingDirection SpriteDirection;
        #endregion

        #region Components
        protected SpriteRenderer spriteRenderer;
        protected Animator anim;
        protected int animationHash = 0;
        protected CharacterController2D controller2D;
		#endregion

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            controller2D = GetComponent<CharacterController2D>();
        }

        protected virtual void Start()
        {
            Initialize();
        }

		/// <summary>
		/// This method initializes the Character2D starting values during scene load or resets.
		/// </summary>
		public virtual void Initialize()
        {
            SetFacingDirection(StartDirection, Vector2.zero);
            SetFlipX();
        }

        protected virtual void LateUpdate()
        {
			SetAnimations();
		}
        public virtual void SetFacingDirection(FacingDirection _facingDirection, Vector2 _direction)
        {
            if(anim is null) return;

            anim.SetFloat("DirectionX", _direction.x);
			anim.SetFloat("DirectionY", _direction.y);
            FacingDirection = _facingDirection;
            SetFlipX();
		}
        protected virtual void SetAnimations()
        {
            stateString = CharacterMoveState.ToString();

            animationHash = Animator.StringToHash(stateString);

            if(anim.HasState(0,animationHash))
            {
                anim.CrossFade(animationHash,0);
            }
        }
        protected void SetFlipX()
        {
            if(SpriteDirection == FacingDirection.Right)
                spriteRenderer.flipX = FacingDirection == FacingDirection.Right ? false : true;
            else
				spriteRenderer.flipX = FacingDirection == FacingDirection.Right ? true : false;
		}
	}
}