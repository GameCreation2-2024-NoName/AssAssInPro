Shader "Shaders/ToonBound"
{
    Properties
    {
        _Outline ("Outline", Range(0, 1)) = 0.1                  //控制轮廓线宽度
        _OutlineColor ("Outline Color", Color) = (1, 0, 0, 1) //轮廓线颜色
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}
        LOD 100

        Pass
        {
            //命名Pass块，以便复用
            NAME "OUTLINE"
            //剔除正面
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            float _Outline;
            fixed4 _OutlineColor;

            struct a2v {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            }; 
            
            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert (a2v v) {
                v2f o;
                float4 pos = mul(UNITY_MATRIX_MV, v.vertex); 
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);  
                normal.z = -0.5;
                pos = pos + float4(normalize(normal), 0) * _Outline;
                o.pos = mul(UNITY_MATRIX_P, pos);
                
                return o;
            }
            
            float4 frag(v2f i) : SV_Target { 
                return float4(_OutlineColor.rgb, 1);               
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}