using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using MoreMountains.Feedbacks;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// Add this component to a character and it'll make your character fall down holes in 2D
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/Abilities/Climb Stairs")]
    public class ClimbStairs : MonoBehaviour
    {
        /// the feedback to play when falling
        [Tooltip("the feedback to play when falling")]
        public MMFeedbacks climbingStairsFeedback;

        /// <summary>
        /// if we find a hole below our character, we kill our character
        /// </summary>
        private void OnTriggerEnter2D(Collider2D col)
        {
            print("Something entered stairs");
            if (col.gameObject.tag == "Player")
            {
                print("Player entered stairs");
                col.gameObject.layer = 10;
                climbingStairsFeedback?.PlayFeedbacks(this.transform.position);
            }
        }
    }
}

