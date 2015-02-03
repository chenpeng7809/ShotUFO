using UnityEngine;
using System.Collections;

public class GameBattleState : AGameState
{
	#region 单例
		private static GameBattleState _instance;

		private GameBattleState ()
		{

		}

		public static GameBattleState GetInstance ()
		{
				if (_instance == null)
						_instance = new GameBattleState ();
				return _instance;
		}

	#endregion
		public override bool PreCheck (object param)
		{
				return true;
		}
	
		public override void Enter (object param)
		{
		
		}
	
		public override void FrameUpdate (object param)
		{
		
		}
	
		public override void Exit (object param)
		{
		
		}
}
