using UnityEngine;
using System.Collections;

public class AUIBase : MonoBehaviour
{

		public virtual void OnOpen (object param)
		{

		}

		public virtual void FrameUpdate (float deltaTime)
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
				FrameUpdate (Time.deltaTime);
		}

		public virtual void OnClose ()
		{

		}
}
