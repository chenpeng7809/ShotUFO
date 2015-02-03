using UnityEngine;
using System.Collections;

public class GameMainMenuState : AGameState
{

	#region 单例
		private static GameMainMenuState _instance;
	
		private GameMainMenuState ()
		{
		
		}
	
		public static GameMainMenuState GetInstance ()
		{
				if (_instance == null)
						_instance = new GameMainMenuState ();
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
