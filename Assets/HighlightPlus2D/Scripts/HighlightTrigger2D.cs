using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighlightPlus2D {


	public enum TriggerMode {
		ColliderEvents = 0,
		Raycast = 1
	}

	public enum RayCastSource {
		MousePosition = 0,
		CameraDirection = 1
	}

	public enum HighlightOnEvent {
		OnOverAndClick = 0,
		OnlyOnOver = 1,
		OnlyOnClick = 2
	}


	[RequireComponent (typeof(HighlightEffect2D))]
	[HelpURL ("https://kronnect.com/support")]
	public class HighlightTrigger2D : MonoBehaviour {

		public HighlightOnEvent highlightEvent = HighlightOnEvent.OnOverAndClick;
		public float highlightDuration;
		[Tooltip ("Used to trigger automatic highlighting including children objects.")]
		public TriggerMode triggerMode = TriggerMode.ColliderEvents;
		public Camera raycastCamera;
		public RayCastSource raycastSource = RayCastSource.MousePosition;

		Collider2D currentCollider;
		[NonSerialized] public readonly List<Collider2D> colliders2D = new List<Collider2D>();

		void OnEnable () {
			Init ();
		}

        private void OnValidate()
        {
			UpdateTriggers();
            
        }

		void UpdateTriggers() {
			if (triggerMode == TriggerMode.Raycast) {
                GetComponentsInChildren(colliders2D);
			}
		}


		void Start () {
			Collider2D collider = GetComponent<Collider2D> ();
			if (collider == null) {
				if (GetComponent<SpriteRenderer> () != null) {
					gameObject.AddComponent<BoxCollider2D> ();
				}
			}
			if (triggerMode == TriggerMode.Raycast) {
				if (raycastCamera == null) {
					raycastCamera = HighlightManager2D.GetCamera ();
					if (raycastCamera == null) {
						Debug.LogError ("Highlight Trigger 2D on " + gameObject.name + ": no camera found!");
					}
				}
				StartCoroutine (DoRayCast ());
			}
		}

		public void Init () {
            InputProxy.Init();
            if (raycastCamera == null) {
				raycastCamera = HighlightManager2D.GetCamera ();
			}
			UpdateTriggers();
		}

		void OnMouseDown () {
			if (highlightEvent != HighlightOnEvent.OnlyOnClick && highlightEvent != HighlightOnEvent.OnOverAndClick)
				return;
			if (triggerMode == TriggerMode.ColliderEvents) {
				Highlight (true);
				if (highlightDuration > 0) {
					CancelInvoke ();
					Invoke ("CancelHighlight", highlightDuration);
				}
			}
		}

		void OnMouseEnter () {
			if (highlightEvent != HighlightOnEvent.OnlyOnOver && highlightEvent != HighlightOnEvent.OnOverAndClick)
				return;
			if (triggerMode == TriggerMode.ColliderEvents) {
				Highlight (true);
				if (highlightDuration > 0) {
					CancelInvoke ();
					Invoke ("CancelHighlight", highlightDuration);
				}
			}
		}

		void OnMouseExit () {
			if (highlightDuration > 0)
				return;
			if (triggerMode == TriggerMode.ColliderEvents) {
				Highlight (false);
			}
		}

		void Highlight (bool state) {
			HighlightEffect2D hb = transform.GetComponent<HighlightEffect2D> ();
			if (hb == null && state) {
				hb = gameObject.AddComponent<HighlightEffect2D> ();
			}
			if (hb != null) {
				hb.SetHighlighted (state);
			}
		}

		void CancelHighlight() {
			Highlight (false);
		}

		IEnumerator DoRayCast () {
			while (triggerMode == TriggerMode.Raycast) {
				if (raycastCamera == null) {
					raycastCamera = HighlightManager2D.GetCamera();
                }
				if (raycastCamera != null) {
					Ray ray;
					if (raycastSource == RayCastSource.MousePosition) {
						ray = raycastCamera.ScreenPointToRay (InputProxy.mousePosition);
					} else {
						ray = new Ray (raycastCamera.transform.position, raycastCamera.transform.forward);
					}
					int layerMask = 1 << gameObject.layer;
					RaycastHit2D hitInfo2D = Physics2D.GetRayIntersection (ray, raycastCamera.farClipPlane, layerMask);
					Collider2D collider = hitInfo2D.collider;
					bool hit = false;
					if (collider != null && colliders2D.Contains(collider)) {
						hit = true;
						if (collider != currentCollider) {
							SwitchCollider (collider);
						}
					}
					if (!hit && currentCollider != null) {
						SwitchCollider (null);
					}
				}
				yield return null;
			}
		}


		void SwitchCollider (Collider2D newCollider) {
			currentCollider = newCollider;
			if (currentCollider != null) {
				Highlight (true);
			} else {
				Highlight (false);
			}
		}


	}

}