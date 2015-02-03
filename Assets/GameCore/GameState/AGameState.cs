using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AGameState : FSState<GameCore>
{
		List<FSState<GameCore>> _canChangeToStateList = new List<FSState<GameCore>> ();

		protected override bool CanChangeStateTo (FSState<GameCore> state)
		{
				return _canChangeToStateList.Contains (state);
		}

		public override void AddCanChangeToState (FSState<GameCore> state)
		{
				if (!_canChangeToStateList.Contains (state))
						_canChangeToStateList.Add (state);
		}
}
