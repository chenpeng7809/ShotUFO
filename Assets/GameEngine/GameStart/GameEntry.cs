using UnityEngine;
using System.Collections;

public class GameEntry : MonoBehaviour
{
		void Awake ()
		{
				AGameStarterBase _starter = GetStarter ();
				if (_starter != null) {
						_starter.Start ();
				} else {
						Debug.LogError ("Game Start Failed");
				}
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				GameCore.GetInstance ().FrameUpdate (Time.deltaTime);
		}

		AGameStarterBase GetStarter ()
		{
				AGameStarterBase starter = null;
				if (Application.isEditor) {

				} else if (Application.isWebPlayer) {

				} else {
						switch (Application.platform) {
						case RuntimePlatform.Android:
								starter = new AndroidStarter ();
								break;
						case RuntimePlatform.WindowsPlayer:
								starter = new WinStandAloneStarter ();
								break;
						}
				}
				return starter;
		}
}
