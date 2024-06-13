Shader "Unlit/NormalsReplacement"{
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
            };

            struct v2f{
                float4 vertex : SV_POSITION;
            };

            v2f Vertex(appdata input){
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                return output;
            }

            fixed4 Fragment(v2f input) : SV_Target{
                return 0;
            }
            ENDCG
        }
    }
}