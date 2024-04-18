using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus2D;

namespace HighlightPlus2D_Demos {
	
	public class Demo : MonoBehaviour {

		GameObject parent;

		void Start () {
			Sprite[] sprites = Resources.LoadAll<Sprite> ("HP2dDemo");

			// Add lot of sprites on the screen
			Camera cam = Camera.main;
			float radius = 0.25f;
			parent = new GameObject ("Icons");

			for (int k = 0; k < sprites.Length; k++) {
				GameObject obj = new GameObject ("Sprite" + k);
				obj.transform.SetParent (parent.transform);
				float angle = Mathf.PI * 8f * k / sprites.Length;
				Vector3 viewportPos = new Vector3 (Mathf.Cos (angle) * radius + 0.5f, Mathf.Sin (angle) * radius * cam.aspect + 0.5f, 10f);
				obj.transform.position = cam.ViewportToWorldPoint (viewportPos);
				SpriteRenderer spr = obj.AddComponent<SpriteRenderer> ();
				spr.sprite = sprites [k];
				obj.AddComponent<PolygonCollider2D> ();
				radius -= 0.0005f;
			}

			HighlightManager2D manager = HighlightManager2D.instance;
			manager.OnObjectHighlightStart += (GameObject obj, ref bool cancelHighlight) => Debug.Log (obj.name + " highlighted!");
		}


		void Update () {
			parent.transform.Rotate (Vector3.forward, Time.deltaTime);
		}
	}

}