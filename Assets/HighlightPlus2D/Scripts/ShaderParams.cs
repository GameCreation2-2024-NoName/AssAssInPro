using UnityEngine;

namespace HighlightPlus2D {

    public static class ShaderParams {
        public static int Color = Shader.PropertyToID("_Color");
        public static int MainTex = Shader.PropertyToID("_MainTex");
        public static int CutOff = Shader.PropertyToID("_CutOff");
        public static int AlphaTex = Shader.PropertyToID("_AlphaTex");
        public static int ZTest = Shader.PropertyToID("_ZTest");

		public static int Pivot = Shader.PropertyToID("_Pivot");
        public static int Geom = Shader.PropertyToID("_Geom");
        public static int UV = Shader.PropertyToID("_UV");
        public static int Flip = Shader.PropertyToID("_Flip");

        public static int OutlineWidth = Shader.PropertyToID("_OutlineWidth");
        public static int OutlineColor = Shader.PropertyToID("_OutlineColor");

        public static int Glow = Shader.PropertyToID("_Glow");
        public static int Glow2 = Shader.PropertyToID("_Glow2");
        public static int GlowColor = Shader.PropertyToID("_GlowColor");

        public static int OverlayBackColor = Shader.PropertyToID("_OverlayBackColor");
        public static int OverlayData = Shader.PropertyToID("_OverlayData");

        public static int SeeThrough = Shader.PropertyToID("_SeeThrough");
        public static int SeeThroughTintColor = Shader.PropertyToID("_SeeThroughTintColor");
        public static int SeeThroughNoise = Shader.PropertyToID("_SeeThroughNoise");

        public static int PivotArray = Shader.PropertyToID("_PivotArray");
        public static int GlowArray = Shader.PropertyToID("_GlowArray");
        public static int GlowColorArray = Shader.PropertyToID("_GlowColorArray");

        public static int Fade = Shader.PropertyToID("_Fade");

		public const string SKW_PIXELSNAP_ON = "PIXELSNAP_ON";
		public const string SKW_POLYGON_PACKING = "POLYGON_PACKING";
		public const string SKW_ETC1_EXTERNAL_ALPHA = "ETC1_EXTERNAL_ALPHA";
		public const string SKW_SMOOTH_EDGES = "SMOOTH_EDGES";
	}

}