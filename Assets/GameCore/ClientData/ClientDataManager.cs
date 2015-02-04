using UnityEngine;
using System.Collections;

public class ClientDataManager
{
	#region 单例
		private static ClientDataManager _instance;
	
		private ClientDataManager ()
		{
		
		}
	
		public static ClientDataManager GetInstance ()
		{
				if (_instance == null)
						_instance = new ClientDataManager ();
				return _instance;
		}
	
	#endregion
}
