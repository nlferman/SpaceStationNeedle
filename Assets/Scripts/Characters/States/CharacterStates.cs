using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSN.Character.States
{
	[System.Flags]
	public enum CharacterMoveState
	{
		None = 0,
		Idle = 1,
		Walking = 2,
		Running = 4,
		Jumping = 8,
	}

	[System.Flags]
	public enum CharacterStatusState
	{
		None = 0,
		Alive = 1,
		Dead = 2,
	}

	public enum CharacterControlState 
	{
		Player,
		AI,
		Event
	}

	public enum FacingDirection
	{
		Right,
		Left,
		Up,
		Down
	}
}
