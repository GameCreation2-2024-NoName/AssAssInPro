using UnityEngine;
using HighlightPlus2D;

namespace HighlightPlus2D.Demos {

    public class PotionHighlightEventSample : MonoBehaviour {

		HighlightEffect2D effect;

		void Start () {
			effect = GetComponent<HighlightEffect2D> ();
			effect.OnObjectHighlightStart += ValidateHighlightObject;
		}

		void Update() {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.position += Vector3.left * Time.deltaTime;
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				transform.position += Vector3.right * Time.deltaTime;
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				effect.HitFX(Color.white, 1f);
            }
		}

		void ValidateHighlightObject (GameObject obj, ref bool cancelHighlight) {
			// Used to fine-control if the object can be highlighted
//			cancelHighlight = true;
		}

		void HighlightStart () {
			Debug.Log ("Potion highlight started!");
		}

		void HighlightEnd () {
			Debug.Log ("Potion highlight ended!");
		}
	}

}