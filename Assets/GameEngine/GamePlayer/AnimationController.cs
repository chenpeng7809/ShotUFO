using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour
{
		private SpriteRenderer spriteRenderer;
		public Sprite[] sprites;
		public float framePerSecond;

		// Use this for initialization
		void Start ()
		{
				spriteRenderer = GetComponent<SpriteRenderer> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				int index = (int)(Time.timeSinceLevelLoad * framePerSecond);
				index = index % sprites.Length;
				spriteRenderer.sprite = sprites [index];
		}
}
