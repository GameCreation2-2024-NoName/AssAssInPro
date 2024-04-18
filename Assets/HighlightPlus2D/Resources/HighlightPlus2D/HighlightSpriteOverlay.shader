Shader "HighlightPlus2D/Sprite/Overlay" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _Color ("Color", Color) = (1,1,1,1)
    _OverlayBackColor ("Overlay Back Color", Color) = (1,1,1,1)
    _OverlayData("Overlay Data", Vector) = (1,0.5,1)
    _CutOff ("Apha CutOff", Float) = 0.05
    _Fade ("Fade", Float) = 1
    _ZTest("ZTest", Int) = 4
}
    SubShader
    {
        Tags { "Queue"="Transparent+10" "RenderType"="Transparent" }
    
        // Overlay
        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Offset -1, -1
            Cull Off
            ZTest [_ZTest]

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ POLYGON_PACKING
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos    : SV_POSITION;
                float2 uv     : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

          	fixed2 _Flip;
      		sampler2D _MainTex;
            sampler2D _AlphaTex;
            float4 _UV;
      		fixed4 _Color;
      		fixed4 _OverlayBackColor;
      		fixed3 _OverlayData; // x = speed, y = MinIntensity, z = blend;
            fixed _CutOff;
            float2 _Pivot;
            fixed _Fade;

            inline float4 UnityFlipSprite(in float4 pos, in fixed2 flip) {
			    return float4(pos.xy * flip, pos.z, 1.0);
			}

            v2f vert (appdata v)
            {
                v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                v.vertex.xy -= _Pivot;
                float4 pos = UnityFlipSprite(v.vertex, _Flip);
                pos = UnityObjectToClipPos(pos);

                #ifdef PIXELSNAP_ON
			        pos = UnityPixelSnap (pos);
    			#endif

    			o.pos = pos;
    			#if POLYGON_PACKING
    				o.uv = v.uv;
    			#else
                	o.uv = lerp(_UV.xy, _UV.zw, v.uv);
                #endif
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
				fixed t = _OverlayData.y + (1.0 - _OverlayData.y) * 2.0 * abs(0.5 - frac(_Time.y * _OverlayData.x));
                fixed4 col = tex2D(_MainTex, i.uv);
			  	#if ETC1_EXTERNAL_ALPHA
    		 	    col.a = tex2D (_AlphaTex, i.uv).a;
			  	#endif

                clip(col.a - _CutOff);
			  	col *= _OverlayBackColor * _Color;
	    		col = lerp(_Color, col, _OverlayData.z);
                col.a *= t;
                col.a *= _Fade;
				return col;
            }
            ENDCG
        }

    }
}