using UnityEngine;
using System.Collections;

public class GameBattleManager
{

	#region 单例
		private static GameBattleManager _instance;
	
		private GameBattleManager ()
		{
		
		}
	
		public static GameBattleManager GetInstance ()
		{
				if (_instance == null)
						_instance = new GameBattleManager ();
				return _instance;
		}
	
	#endregion

		private float _runTime = 0;

		public float runTime {
				get {
						return _runTime;
				}
		}
}
