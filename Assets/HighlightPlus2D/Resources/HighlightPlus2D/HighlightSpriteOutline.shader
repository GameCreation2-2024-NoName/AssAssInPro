Shader "HighlightPlus2D/Sprite/Outline" {
Properties {
    _MainTex ("Texture", 2D) = "white" {}
    _OutlineColor ("Outline Color", Color) = (0,0,0,1)
    _OutlineWidth ("Outline Offset", Float) = 0.01
    _CutOff ("Apha CutOff", Float) = 0.05
    _Color ("Color", Color) = (1,1,1) // not used; dummy property to avoid inspector warning "material has no _Color property"
    _Fade ("Fade", Float) = 1
    _ZTest("ZTest", Int) = 4
}
    SubShader
    {
        Tags { "Queue"="Transparent+3" "RenderType"="TransparentCutout" "DisableBatching"="True" }

        // Black outline
        Pass
        {

            Stencil {
                Ref 32
                Comp NotEqual
                Pass keep 
                ReadMask 32
            }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            ZTest [_ZTest]

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #pragma multi_compile _ POLYGON_PACKING
            #pragma multi_compile _ SMOOTH_EDGES
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
                float2 pos0: TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

		    fixed2 _Flip;
            sampler2D _MainTex;
            sampler2D _AlphaTex;
            float4 _UV;
            float4 _MainTex_TexelSize;
            float4 _AlphaTex_TexelSize;
            fixed _CutOff;
            fixed4 _OutlineColor;
            float _OutlineWidth;
            fixed _Fade;

            float2 _Pivot;
            float3 _Geom;

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
                o.pos0 = pos.xy;

                float z = pos.w;
                float offset = z * _OutlineWidth;
                float4 posZero  = UnityObjectToClipPos(float4(_Geom.xy,0,1));
                float2 dir = pos.xy - posZero.xy;
                dir.y *= _Geom.z;
                pos.xy += normalize(dir) * offset;
                
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
  		      fixed4 col = _OutlineColor;
  		      #if SMOOTH_EDGES
  		      	fixed neighbours;
  		      	#if ETC1_EXTERNAL_ALPHA
	              	neighbours  = tex2D(_AlphaTex, i.uv + fixed2(0, _AlphaTex_TexelSize.y)).a;
    	          	neighbours += tex2D(_AlphaTex, i.uv - fixed2(0, _AlphaTex_TexelSize.y)).a;
        	      	neighbours += tex2D(_AlphaTex, i.uv + fixed2(_AlphaTex_TexelSize.x, 0)).a;
            	  	neighbours += tex2D(_AlphaTex, i.uv - fixed2(_AlphaTex_TexelSize.x, 0)).a;
              	#else
	              	neighbours  = tex2D( _MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
              		neighbours += tex2D( _MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
              		neighbours += tex2D( _MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
              		neighbours += tex2D( _MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0)).a;
              	#endif
              	neighbours = saturate(neighbours * 0.25 - _CutOff);
              	col.a *= neighbours;
  		      #else
  		      	fixed4 neighbours;
  		      	#if ETC1_EXTERNAL_ALPHA
	              	neighbours.x = tex2D(_AlphaTex, i.uv + fixed2(0, _AlphaTex_TexelSize.y)).a;
    	          	neighbours.y = tex2D(_AlphaTex, i.uv - fixed2(0, _AlphaTex_TexelSize.y)).a;
        	      	neighbours.z = tex2D(_AlphaTex, i.uv + fixed2(_AlphaTex_TexelSize.x, 0)).a;
            	  	neighbours.w = tex2D(_AlphaTex, i.uv - fixed2(_AlphaTex_TexelSize.x, 0)).a;
              	#else
	              	neighbours.x = tex2D( _MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
              		neighbours.y = tex2D( _MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
              		neighbours.z = tex2D( _MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
              		neighbours.w = tex2D( _MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0)).a;
              	#endif
              	neighbours = saturate(neighbours - _CutOff);
  		      	col *= any(neighbours);
  		      #endif
              col.a *= _Fade;
  		      return col;
            }
            ENDCG
        }

    }
}