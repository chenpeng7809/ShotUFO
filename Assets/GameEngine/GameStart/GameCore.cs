using UnityEngine;
using System.Collections;

public class GameCore
{
	#region singleton
		private static GameCore _instance;
		private static readonly object _lock = new object ();

		private GameCore ()
		{

		}

		public static GameCore GetInstance ()
		{
				if (_instance == null) {
						lock (_lock) {
								if (_instance == null)
										_instance = new GameCore ();
						}
				}
				return _instance;
		}

	#endregion

		public void Startup ()
		{

		}

		public void Terminate ()
		{

		}

		public void FrameUpdate (float deltaTime)
		{

		}

	#region 游戏状态机
		StateMachine<GameCore> _gameStateMachine;
	#endregion

}
