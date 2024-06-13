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
            // Make fog work
            #pragma multi_compile_fog

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
                UNITY_TRANSFER_FOG(output, output.vertex);
                return output;
            }

            fixed4 Fragment(v2f input) : SV_Target{
                // Apply fog
                UNITY_APPLY_FOG(input.fogCoord, colour);
                return 0;
            }
            ENDCG
        }
    }
}