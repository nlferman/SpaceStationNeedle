using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;

namespace MoreMountains.Tools
{
    /// <summary>
    /// An event to trigger when a checkpoint is reached
    /// </summary>
    public struct LoseAbilityEvent
    {
        public string Ability;
        public LoseAbilityEvent(string ability)
        {
            Ability = ability;
        }

        static LoseAbilityEvent e;
        public static void Trigger(string ability)
        {
            e.Ability = ability;
            MMEventManager.TriggerEvent(e);
        }
    }

    public class CharacterAbilityEvents : MonoBehaviour
    {
        public string abilityName;

        public void TriggerAbilityLoss()
        {
            LoseAbilityEvent.Trigger(abilityName);
        }
    }
}
