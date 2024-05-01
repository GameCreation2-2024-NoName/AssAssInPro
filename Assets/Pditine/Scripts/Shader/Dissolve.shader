Shader "Pditine/Dissolve"
{
    Properties
    {
//        _Diffuse ("Diffuse",Color) = (1,1,1,1)
//        _Gloss ("Gloss",Range(8.0,256)) = 20
//        _Specular ("Specular",Color) = (1,1,1,1)
        //_Color ("Main Tint",Color) = (1,1,1,1)
        _MainTex("Main Tex",2D) = "white"{}
        _CutoffTex("_CutoffTex",2D) = "white"{}
        _Cutoff("Alpha Cutoff",Range(0,1)) =  0.5
    }
    SubShader
    {
        Tags{ "Queue"= "AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
        Pass
        {
            Tags{ "LightMode" = "ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #include "Lighting.cginc"

            //fixed4 _Color;
            sampler2D _MainTex;
            sampler2D _CutoffTex;
            fixed _Cutoff;
            float4 _MainTex_ST;
            struct a2v
            {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            v2f vert (a2v v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex,i.uv);
                clip(tex2D(_CutoffTex,i.uv).r - _Cutoff);
                return texColor;
            }
            ENDCG
        }
    }
FallBack "Transparent/Cutout/VertexLit"
}
