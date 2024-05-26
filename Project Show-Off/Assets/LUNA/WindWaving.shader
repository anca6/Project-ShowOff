Shader "Luna/WindWaving"{
    Properties{
        _MainTex ("Texture", 2D) = "white" {}
        _WorldSize ("World Size", Vector) = (1, 1, 1, 1)
    }
    SubShader{
        Tags{
            "RenderType" = "Opaque"
        }
        LOD 100

        Pass{
            CGPROGRAM
            #pragma vertex vertex
            #pragma fragment fragment
            // Make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            CBUFFER_START(UnityPerMaterial)
            float4 _WorldSize;
            CBUFFER_END

            v2f vertex (appdata input){
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                UNITY_TRANSFER_FOG(output, output.vertex);
                return output;
            }

            fixed4 fragment (v2f input) : SV_Target{
                // Sample the texture
                fixed4 colour = tex2D(_MainTex, input.uv);
                // Apply fog
                UNITY_APPLY_FOG(input.fogCoord, colour);
                return colour;
            }
            ENDCG
        }
    }
}