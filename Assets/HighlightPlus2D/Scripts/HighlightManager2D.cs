using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighlightPlus2D {


	[RequireComponent (typeof(HighlightEffect2D))]
	[HelpURL ("https://kronnect.com/support")]
	public class HighlightManager2D : MonoBehaviour {

		public event OnObjectHighlightStartEvent OnObjectHighlightStart;
		public event OnObjectHighlightEndEvent OnObjectHighlightEnd;

		public HighlightOnEvent highlightEvent = HighlightOnEvent.OnOverAndClick;
		[Tooltip("Max duration for the highlight event")]
		public float highlightDuration;

		public LayerMask layerMask = -1;
		public Camera raycastCamera;
		public RayCastSource raycastSource = RayCastSource.MousePosition;

		HighlightEffect2D baseEffect, currentEffect;
		Transform currentObject;

		static HighlightManager2D _instance;
		public static HighlightManager2D instance {
			get {
				if (_instance == null) {
					_instance = Misc.FindObjectOfType<HighlightManager2D> ();
				}
				return _instance;
			}
		}

		void OnEnable () {
			InputProxy.Init();
			currentObject = null;
			currentEffect = null;
			if (baseEffect == null) {
				baseEffect = GetComponent<HighlightEffect2D> ();
				if (baseEffect == null) {
					baseEffect = gameObject.AddComponent<HighlightEffect2D> ();
				}
			}
			raycastCamera = GetComponent<Camera> ();
			if (raycastCamera == null) {
				raycastCamera = GetCamera ();
				if (raycastCamera == null) {
					Debug.LogError ("Highlight Manager 2D: no camera found!");
				}
			}
		}


		void OnDisable () {
			SwitchesCollider (null);
		}

		void Update () {

			if (highlightEvent == HighlightOnEvent.OnlyOnClick && !Input.GetMouseButtonDown(0))
				return;

			if (raycastCamera == null) {
				raycastCamera = GetCamera();
				if (raycastCamera == null)
					return;
			}

			Ray ray;
			if (raycastSource == RayCastSource.MousePosition) {
				ray = raycastCamera.ScreenPointToRay (InputProxy.mousePosition);
			} else {
				ray = new Ray (raycastCamera.transform.position, raycastCamera.transform.forward);
			}
			RaycastHit2D hitInfo2D = Physics2D.GetRayIntersection (ray, raycastCamera.farClipPlane, layerMask);
			Collider2D collider = hitInfo2D.collider;
			if (collider != null) {
				if (collider.transform != currentObject) {
					SwitchesCollider (collider.transform);
				}
				return;
			}

			// no hit
			if (highlightDuration == 0) {
				SwitchesCollider (null);
			}
		}

		void SwitchesCollider (Transform newObject) {
			if (currentEffect != null) {
				if (OnObjectHighlightEnd != null) {
					OnObjectHighlightEnd (currentEffect.gameObject);
				}
				currentEffect.SetHighlighted (false);
				currentEffect = null;
			}
			currentObject = newObject;
			if (newObject == null)
				return;
			HighlightTrigger2D ht = newObject.GetComponent<HighlightTrigger2D> ();
			if (ht != null && ht.enabled)
				return;

			if (OnObjectHighlightStart != null) {
				bool cancelHighlight = false;
				OnObjectHighlightStart (newObject.gameObject, ref cancelHighlight);
				if (cancelHighlight)
					return;
			}

			HighlightEffect2D otherEffect = newObject.GetComponent<HighlightEffect2D> ();
			currentEffect = otherEffect != null ? otherEffect : baseEffect;
			currentEffect.SetTarget (currentObject.transform);
			currentEffect.SetHighlighted (true);
			if (highlightEvent > 0) {
				CancelInvoke ();
				Invoke ("CancelHighlight", highlightDuration);
			}
		}

		void CancelHighlight() {
			SwitchesCollider (null);
		}

		public static Camera GetCamera () {
			Camera raycastCamera = Camera.main;
			if (raycastCamera == null) {
				raycastCamera = Misc.FindObjectOfType<Camera> ();
			}
			return raycastCamera;
		}


	}

}