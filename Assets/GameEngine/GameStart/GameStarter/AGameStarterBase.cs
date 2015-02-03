using UnityEngine;
using System.Collections;

public abstract class AGameStarterBase
{
		public abstract void Setup ();

		public abstract void Start ();

		protected void StartGame ()
		{
				GameCore.GetInstance ().Startup ();
		}

}
