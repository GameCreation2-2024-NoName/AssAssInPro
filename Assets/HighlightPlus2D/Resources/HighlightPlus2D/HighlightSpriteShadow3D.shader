Shader "HighlightPlus2D/Sprite/Shadow3D" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _Color ("Shadow Color", Color) = (0,0,0,0.2)
}
    SubShader
    {
        // Shadow 3D
		Pass {
 				Name "ShadowCaster"
				Tags { "LightMode" = "ShadowCaster" }
		        Offset 2, 2
		        Cull Off

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
            float2 _Pivot;

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
                fixed4 col = tex2D(_MainTex, i.uv);
			  	#if ETC1_EXTERNAL_ALPHA
    		 	    col.a = tex2D (_AlphaTex, i.uv).a;
			  	#endif

			  	clip(col.a - 0.25);
			  	return 0;
            }
            ENDCG
		}



    }
}