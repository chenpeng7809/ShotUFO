using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleManager : MonoBehaviour
{
	#region 单例
		private static ModuleManager _instance;
	
		private ModuleManager ()
		{
		
		}
	
		public static ModuleManager GetInstance ()
		{
				if (_instance == null)
						_instance = new ModuleManager ();
				return _instance;
		}
	
	#endregion

		static Dictionary<ModuleId,string> uiNameMap = new Dictionary<ModuleId, string> ()
	{
		{ModuleId.MainMenu,"MainMenu" },
		{ModuleId.Setting,"Setting" },
		{ModuleId.Fight,"Fight" },
	};
		Dictionary<ModuleId,AUIBase> moduleList = new Dictionary<ModuleId, AUIBase> ();
		const string UIResoucePath = "";

		void InitModuleList ()
		{

		}

		public void Startup ()
		{

		}

		public void Terminate ()
		{

		}

		public void OpenUI (ModuleId moduleId, object param = null)
		{
				if (moduleList.ContainsKey (moduleId)) {
						moduleList [moduleId].OnOpen (param);
				} else {
						//TODO 加载UI资源
						Object prefab = Resources.LoadAssetAtPath (UIResoucePath + uiNameMap[moduleId], typeof(GameObject));
						GameObject cube = (GameObject)GameObject.Instantiate (prefab);
						cube.transform.parent = transform;

						moduleList [moduleId].OnOpen (param);
				}
		}

		public void CloseUI (ModuleId moduleId, object param = null)
		{
				if (moduleList.ContainsKey (moduleId)) {
						moduleList [moduleId].OnClose (param);
				} else {
						Debug.LogError ("Close UI failded, because module[" + moduleId + "]didn't exist");
				}
		}
}
