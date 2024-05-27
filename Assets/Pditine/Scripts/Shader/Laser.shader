
Shader "Pditine/Laser" {
Properties {
	_MainTex("Main Texture", 2D) = "white" {}
	_Color("Color",Color) = (1,1,1,1)
	_NoiseTex("NoiseTex",2D) = "white"{}
	_Concentrate("_Concentrate",Range(1,20)) = 2
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			float _Concentrate;
			fixed4 _Color;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv)*_Color;
				fixed baseAlpha = -4*i.uv.y*i.uv.y+ 4*i.uv.y;
				//todo:激光流动效果
				//fixed alpha = pow(baseAlpha,_Concentrate)* tex2D(_NoiseTex,i.uv.y*_Time.w-floor(i.uv.y*_Time.w)).r;
				fixed alpha = pow(baseAlpha,_Concentrate);
				return fixed4(col.rgb,alpha);
			}
			ENDCG 
		}
	}	
}
}