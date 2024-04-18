Shader "HighlightPlus2D/Sprite/SeeThrough" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _CutOff ("Apha CutOff", Float) = 0.05
    _SeeThrough ("See Through", Range(0,1)) = 0.8
    _SeeThroughTintColor ("See Through Tint Color", Color) = (1,0,0,0.8)
    _Color ("Color", Color) = (1,1,1) // not used; dummy property to avoid inspector warning "material has no _Color property"
    _SeeThroughNoise("Noise", Range(0, 1)) = 1
    _Fade ("Fade", Float) = 1
}
    SubShader
    {
        Tags { "Queue"="Transparent+101" "RenderType"="Transparent" }
   
        // See through effect
        Pass
        {
            ZTest Greater
            ZWrite Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha

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
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

           	fixed2 _Flip;
           	fixed _CutOff;
            sampler2D _MainTex;
            float4 _UV;
            sampler2D _AlphaTex;
            
            float2 _Pivot;

            fixed _SeeThrough;
            fixed4 _SeeThroughTintColor;
            fixed _SeeThroughNoise;

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
                float2 scrPos = i.pos.xy;

                fixed4 col = tex2D(_MainTex, i.uv);
                #if ETC1_EXTERNAL_ALPHA
                    col.a = tex2D (_AlphaTex, i.uv).a;
                #endif

                clip(col.a - _CutOff);
                col.rgb = lerp(col.rgb, _SeeThroughTintColor.rgb, _SeeThroughTintColor.a);
                col.rgb += _SeeThroughNoise * (frac(scrPos.y * _Time.w) * 0.1);
                col.a = _SeeThrough;
            	col.a = lerp(col.a, saturate(col.a * ((scrPos.y % 2) - 1.0)), _SeeThroughNoise);
                col.a *= _Fade;
                return col;
            }
            ENDCG
        }

    }
}