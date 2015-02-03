using UnityEngine;
using System.Collections;

public abstract class FSState<T>
{

		public bool CanChangeState (FSState<T> state)
		{
				if (CanChangeStateTo (state))
						return true;
				else
						return false;
		}

		public abstract bool PreCheck (object param);

		public abstract void Enter (object param);

		public abstract void FrameUpdate (object param);

		public abstract void Exit (object param);

		protected T _owner;
		protected StateMachine<T> _fsm;

		protected abstract bool CanChangeStateTo (FSState<T> state);

		public abstract void AddCanChangeToState (FSState<T> state);

		public void Setup (T owner, StateMachine<T> fsm)
		{
				_owner = owner;
				_fsm = fsm;
		}
}
