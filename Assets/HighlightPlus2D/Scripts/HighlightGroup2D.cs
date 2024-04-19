using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighlightPlus2D {


    /// <summary>
    /// Use this behaviour to clear stencil before a sprite group starts rendering. Useful to control group outlines.
    /// </summary>
	[RequireComponent(typeof(HighlightEffect2D))]
    [HelpURL("http://kronnect.com/taptapgo")]
    [ExecuteInEditMode]
    public class HighlightGroup2D : MonoBehaviour {

        [Range(0, 16)]
        public int groupNumber;

        public const int GROUP_OFFSET = 5;
        static int frameCleared;
        static Material clearStencil;

        private void OnEnable() {
            if (clearStencil == null) {
                clearStencil = new Material(Shader.Find("HighlightPlus2D/Sprite/ClearStencil"));
            }
        }

        private void OnValidate() {
            HighlightEffect2D[] effects = Misc.FindObjectsOfType<HighlightEffect2D>();
            for (int k=0;k<effects.Length;k++) {
                effects[k].Refresh();
            }
        }

        void Update() {
            if (frameCleared == Time.frameCount || groupNumber == 0) return;
            frameCleared = Time.frameCount;
            clearStencil.renderQueue = 3000 - 11 + groupNumber * GROUP_OFFSET;
            Matrix4x4 m = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(10000, 10000, 1));
            Graphics.DrawMesh(HighlightEffect2D.GetQuadMesh(), m, clearStencil, gameObject.layer);
        }



    }

}