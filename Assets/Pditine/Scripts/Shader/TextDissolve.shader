Shader "Pditine/TextDissolve"
{
    Properties
    {
        _FaceTex("Face Texture", 2D) = "white" {}
        _CutMaskTex("cutMaxkTex",2D) = "white"{}
        _Cutoff("_Cutoff",Range(0,1)) = 0
        _LineWidth("LineWidth",Float) = 0.01
        _LineColor("LineColor",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _FaceTex;
            sampler2D _CutMaskTex;
            float4 _MainTex_ST;
            float _Cutoff;
            float _LineWidth;
            fixed4 _LineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_FaceTex, i.uv);
                fixed4 mask = tex2D(_CutMaskTex,i.uv);
                clip(mask.r - _Cutoff);
                if(mask.r-_Cutoff-_LineWidth<0)
                    color.rgb=_LineColor.rgb;
                return color;
            }
            ENDCG
        }
    }
Fallback "TextMeshPro/Mobile/Distance Field"
}
