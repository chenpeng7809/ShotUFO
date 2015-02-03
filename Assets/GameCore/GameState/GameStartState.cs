using UnityEngine;
using System.Collections;

public class GameStartState : AGameState
{

	#region 单例
		private static GameStartState _instance;
	
		private GameStartState ()
		{
		
		}
	
		public static GameStartState GetInstance ()
		{
				if (_instance == null)
						_instance = new GameStartState ();
				return _instance;
		}
	
	#endregion
		public override bool PreCheck (object param)
		{
				return true;
		}

		public override void Enter (object param)
		{
				_fsm.ChangeState (typeof(GameMainMenuState));
		}

		public override void FrameUpdate (object param)
		{

		}

		public override void Exit (object param)
		{

		}
}
