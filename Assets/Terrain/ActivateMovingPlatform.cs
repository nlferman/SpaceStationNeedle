using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.TopDownEngine
{
    public class ActivateMovingPlatform : MovingPlatform2D, MMEventListener<LoseAbilityEvent>
    {
        private ParticleSystem particles;

        private void Start()
        {
            particles = GetComponent<ParticleSystem>();
            particles.Stop();
        }

        public void OnMMEvent(LoseAbilityEvent abilityLost)
        {
            print("LoseAbilityEvent Triggered " + abilityLost.Ability);
            if (abilityLost.Ability == "Hover")
            {
                print("Activating Moving Platforms");
                CanMove = true;
                particles.Play();
            }
        }

        private void OnEnable()
        {
            this.MMEventStartListening<LoseAbilityEvent>();
        }

        private void OnDisable()
        {
            this.MMEventStopListening<LoseAbilityEvent>();
        }
    }
}
