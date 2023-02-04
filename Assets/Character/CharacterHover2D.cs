using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// Add this ability to a character and it'll be able to hover in 2D.
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/Abilities/Character Hover 2D")]
    public class CharacterHover2D : CharacterAbilityEvents, MMEventListener<LoseAbilityEvent>
	{
        /// the duration of the hover
        [Tooltip("the duration of the hover")]
        public float MaxHoverDuration = 5f;
        /// whether or not jump should be proportional to press (if yes, releasing the button will stop the jump)
        [Tooltip("whether or not jump should be proportional to press (if yes, releasing the button will stop the jump)")]
        public bool HoverProportionalToPress = true;
        /// the minimum amount of time after the jump starts before releasing the jump has any effect
        [Tooltip("the minimum amount of time after the jump starts before releasing the jump has any effect")]
        public float MinimumPressTime = 0.2f;
        /// the feedback to play when the jump starts
        [Tooltip("the feedback to play when the jump starts")]
        public MMFeedbacks HoverStartFeedback;
        /// the feedback to play when the jump stops
        [Tooltip("the feedback to play when the jump stops")]
        public MMFeedbacks HoverStopFeedback;

        protected CharacterButtonActivation _characterButtonActivation;
        protected int _characterButtonPressCount = 0;
        protected bool _hoverStopped = false;
        protected float _hoverStartedAt = 0f;
        protected bool _buttonReleased = false;
        protected const string _hoveringAnimationParameterName = "Hovering";
        protected const string _hitTheGroundAnimationParameterName = "HitTheGround";
        protected int _hoveringAnimationParameter;
        protected int _fallingAnimationParameter;

        /// <summary>
        /// On init we grab our components
        /// </summary>
        protected override void Initialization()
		{
			base.Initialization ();
			_characterButtonActivation = GetComponent<CharacterButtonActivation> ();
            HoverStartFeedback?.Initialization(this.gameObject);
            HoverStopFeedback?.Initialization(this.gameObject);
		}

        /// <summary>
        /// On HandleInput we watch for hover input and trigger a hover if needed
        /// </summary>
		protected override void HandleInput()
		{
			base.HandleInput();
            if (_movement.CurrentState == CharacterStates.MovementStates.Idle) {
                _characterButtonPressCount = 0;
                _buttonReleased = false;
            }
            // if movement is prevented, or if the character is dead/frozen/can't move, we exit and do nothing
            if (!AbilityPermitted
                || (_condition.CurrentState != CharacterStates.CharacterConditions.Normal))
                // || (_movement.CurrentState != CharacterStates.MovementStates.Jumping))
                // || (_movement.CurrentState == CharacterStates.MovementStates.Hovering && !_buttonReleased))
            {
                return;
            }
            if (_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonDown)
			{
                if (_characterButtonPressCount >= 1 && _characterButtonPressCount < 3) {
                    HoverStart();
                }
            }
            if (_inputManager.JumpButton.State.CurrentState == MMInput.ButtonStates.ButtonUp)
            {
                ++_characterButtonPressCount;
                if (_characterButtonPressCount > 0) {
                    _buttonReleased = true;
                }
            }
        }

        /// <summary>
        /// On process ability, we stop the hover if needed
        /// </summary>
        public override void ProcessAbility()
        {
            if (_movement.CurrentState == CharacterStates.MovementStates.Hovering)
            {
                if (!_hoverStopped)
                {
                    if (Time.time - _hoverStartedAt >= MaxHoverDuration)
                    {
                        HoverStop();
                    }
                    else
                    {
                        _movement.ChangeState(CharacterStates.MovementStates.Hovering);
                    }
                }
                if (_buttonReleased
                   && !_hoverStopped
                   && HoverProportionalToPress
                   && (Time.time - _hoverStartedAt > MinimumPressTime))
                {
                    HoverStop();
                }
            }
        }

        /// <summary>
        /// Starts a jump
        /// </summary>
		protected virtual void HoverStart()
		{
			if (!EvaluateJumpConditions())
			{
				return;
			}
			_movement.ChangeState(CharacterStates.MovementStates.Hovering);	
			MMCharacterEvent.Trigger(_character, MMCharacterEventTypes.Hover);
            gameObject.layer = 23;
            HoverStartFeedback?.PlayFeedbacks(this.transform.position);
            PlayAbilityStartFeedbacks();

            _hoverStopped = false;
            _hoverStartedAt = Time.time;
            _buttonReleased = false;
        }

        /// <summary>
        /// Stops hovering
        /// </summary>
        protected virtual void HoverStop()
        {
            _hoverStopped = true;
            _movement.ChangeState(CharacterStates.MovementStates.Falling);
            gameObject.layer = 10;
            _buttonReleased = false;
            _characterButtonPressCount = 0;
            HoverStopFeedback?.PlayFeedbacks(this.transform.position);
            StopStartFeedbacks();
            PlayAbilityStopFeedbacks();
        }

        /// <summary>
        /// Returns true if jump conditions are met
        /// </summary>
        /// <returns></returns>
		protected virtual bool EvaluateJumpConditions()
		{
			if (!AbilityPermitted)
			{
				return false;
			}
			if (_characterButtonActivation != null)
			{
				if (_characterButtonActivation.AbilityPermitted
					&& _characterButtonActivation.InButtonActivatedZone)
				{
					return false;
				}
			}
            if (_movement.CurrentState != CharacterStates.MovementStates.Jumping)
            {
                return false;
            }
			return true;
		}

		/// <summary>
		/// Adds required animator parameters to the animator parameters list if they exist
		/// </summary>
		protected override void InitializeAnimatorParameters()
		{
			RegisterAnimatorParameter (_hoveringAnimationParameterName, AnimatorControllerParameterType.Bool, out _hoveringAnimationParameter);
			RegisterAnimatorParameter (_hitTheGroundAnimationParameterName, AnimatorControllerParameterType.Bool, out _fallingAnimationParameter);
		}

		/// <summary>
		/// At the end of each cycle, sends Jumping states to the Character's animator
		/// </summary>
		public override void UpdateAnimator()
		{
            MMAnimatorExtensions.UpdateAnimatorBool(_animator, _hoveringAnimationParameter, (_movement.CurrentState == CharacterStates.MovementStates.Hovering),_character._animatorParameters);
            MMAnimatorExtensions.UpdateAnimatorBool (_animator, _fallingAnimationParameter, (_movement.CurrentState == CharacterStates.MovementStates.Falling), _character._animatorParameters);
		}

        public void OnMMEvent(LoseAbilityEvent abilityLost)
        {
            print("LoseAbilityEvent Triggered " + abilityLost.Ability);
            if (abilityLost.Ability == "Hover")
                {
                    print("Deactivating Hover");
                    AbilityPermitted = false;
                }
        } 

        protected override void OnEnable()
        {
            this.MMEventStartListening<LoseAbilityEvent>();
        }

        protected override void OnDisable()
        {
            this.MMEventStopListening<LoseAbilityEvent>();
        } 
	}
}
