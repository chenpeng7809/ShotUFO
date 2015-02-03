using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<T>
{
		/// <summary>
		/// 当前状态
		/// </summary>
		private FSState<T> _currentState;

		public FSState<T> currentState {
				get {
						return _currentState;
				}
		}
		/// <summary>
		/// 上一个状态
		/// </summary>
		private FSState<T> _previousState;

		public FSState<T> previousState {
				get {
						return _previousState;
				}
		}

		protected Dictionary<System.Type,FSState<T>> _stateDic = new Dictionary<System.Type, FSState<T>> ();
		protected T _owner;

		public StateMachine (T owner)
		{
				_owner = owner;
				_currentState = null;
				_previousState = null;
		}

		public void Startup ()
		{

		}

		public void FrameUpdate (float deltaTime)
		{
				if (_currentState != null)
						_currentState.FrameUpdate (deltaTime);
		}

		public void Terminate ()
		{

		}

		public void RegistState (FSState<T> state)
		{
				state.Setup (_owner, this);
				if (_stateDic.ContainsKey (state.GetType ())) {
						_stateDic [state.GetType ()] = state;
				} else {
						_stateDic.Add (state.GetType (), state);
				}
		}

		public void RegistTransition (FSState<T> source, FSState<T> dest, bool isTwoWay = false)
		{
				if (!_stateDic.ContainsKey (source.GetType ()))
						return;
				if (!_stateDic.ContainsKey (dest.GetType ()))
						return;
				source.AddCanChangeToState (dest);
				if (isTwoWay)
						dest.AddCanChangeToState (source);
		}

		public bool isInState (FSState<T> state)
		{
				if (!_stateDic.ContainsKey (state.GetType ()))
						return false;
				if (_currentState.GetType () == state.GetType ())
						return true;
				else
						return false;
		}

		public void ChangeState (System.Type stateType, object param = null)
		{
				try {
						if (_stateDic.ContainsKey (stateType)) {
								FSState<T> newState = null;
								if (_stateDic.TryGetValue (stateType, out newState)) {
										if (_currentState.CanChangeState (newState) && newState.PreCheck (param))
												InternalChangeState (newState, param);
										else
												Debug.LogWarning ("state[" + _currentState.GetType () + "]change to state[" + stateType + "]failed");
								} else {
										if (newState.PreCheck (param))
												InternalChangeState (newState, param);
								}
						} else {
								Debug.LogError ("The state [" + stateType + "] have not be registed");
						}
				} catch (System.Exception e) {
						Debug.LogError (e.ToString ());
				}
		}

		protected void InternalChangeState (FSState<T> newState, object param)
		{
				_previousState = _currentState;
				_currentState = newState;
				if (_previousState != null)
						_previousState.Exit (param);
				if (_currentState != null)
						_currentState.Enter (param);
		}
}
