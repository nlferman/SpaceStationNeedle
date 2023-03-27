using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSN
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;


		#region Starting Lifecycle Functions
		private void Awake()
		{
			if(Instance != null && Instance != this)
			{
				Destroy(this);
				return;
			}
			Instance = this;
		}
		#endregion
	}
}