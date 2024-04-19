Shader "HighlightPlus2D/Sprite/Mask" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _CutOff ("Apha CutOff", Float) = 0.05
    _Color ("Color", Color) = (1,1,1) // not used; dummy property to avoid inspector warning "material has no _Color property"
    _ZTest("ZTest", Int) = 4
}
    SubShader
    {
        Tags { "Queue"="Transparent+1" "RenderType"="TransparentCutout" }

        // Create mask
        Pass
        {
			Stencil {
                Ref 32
                Comp always
                Pass replace
                WriteMask 32
            }
            ColorMask 0
            ZWrite Off
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

            sampler2D _MainTex;
            sampler2D _AlphaTex;
            fixed2 _Flip;
            fixed _CutOff;
            float4 _UV;
            float2 _Pivot;
            
			inline float4 UnityFlipSprite(in float4 pos, in fixed2 flip) {
			    return float4(pos.xy * flip, pos.z, 1.0);
			}

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos: SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert (appdata v)
            {
                v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_OUTPUT(v2f, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                v.vertex.xy -= _Pivot;
            	o.pos = UnityFlipSprite(v.vertex, _Flip);
                o.pos = UnityObjectToClipPos(o.pos);

                #ifdef PIXELSNAP_ON
			        o.pos = UnityPixelSnap (o.pos);
    			#endif

    			#if POLYGON_PACKING
    				o.uv = v.uv;
    			#else
                	o.uv = lerp(_UV.xy, _UV.zw, v.uv);
                #endif
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
            	fixed4 color = tex2D(_MainTex, i.uv);
				#if ETC1_EXTERNAL_ALPHA
    			    color.a = tex2D (_AlphaTex, i.uv).a;
				#endif

            	clip(color.a - _CutOff);
            	return 0;
            }
            ENDCG
        }

    }
}