using UnityEngine;
using System.Collections;

public class GameScene : MonoBehaviour
{
	#region 单例
		private static GameScene _instance;
	
		private GameScene ()
		{
		
		}
	
		public static GameScene GetInstance ()
		{
				if (_instance == null)
						_instance = new GameScene ();
				return _instance;
		}
	
	#endregion

		private bool _loadSceneFlag = false;

		public void InitScene ()
		{
				if (!_loadSceneFlag)
						LoadScene ();
		}

		void LoadScene ()
		{
				_loadSceneFlag = true;
				Object prefab = Resources.LoadAssetAtPath ("Assets/Scene/scene.prefab", typeof(GameObject));
				GameObject cube = (GameObject)Instantiate (prefab);
				cube.transform.parent = transform;
		}

		UISprite bgLeft;
		UISprite bgRight;

		void Update ()
		{
				//背景滚动
				Vector3 posLeft = bgLeft.transform.localPosition;
				Vector3 posRight = bgRight.transform.localPosition;
				posLeft.x -= 2;
				posRight.x -= 2;
				bgLeft.transform.localPosition = posLeft;
				bgRight.transform.localPosition = posRight;
				if (posRight.x == 0) {
						bgLeft.transform.localPosition = new Vector3 (bgRight.width, 0, 0);
				}
				if (posLeft.x == 0) {
						bgRight.transform.localPosition = new Vector3 (bgLeft.width, 0, 0);
				}
		}

}
