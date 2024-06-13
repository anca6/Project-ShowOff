Shader "Luna/NormalsReplacement"{
    Properties{}
    SubShader{
        Tags{
            "RenderType" = "Opaque"
        }
        LOD 100

        Pass{
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "UnityCG.cginc"

            struct appdata{
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f{
                float4 vertex : SV_POSITION;
                float3 viewNormal : NORMAL;
            };

            v2f Vertex(appdata input){
                appdata v = input;
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.viewNormal = COMPUTE_VIEW_NORMAL;
                return output;
            }

            fixed4 Fragment(v2f input) : SV_Target{
                return float4(input.viewNormal, 0);
            }
            ENDCG
        }
    }
}