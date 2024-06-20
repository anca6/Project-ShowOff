Shader "Luna/Occlusion"{
    Properties{
        _Colour ("Colour", Color) = (1, 1, 1, 1)
        _MainTexture ("Texture", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0
    }
    SubShader{
        Tags{
            "RenderType" = "Opaque"
            "Queue" = "Overlay"
        }
        LOD 100

        Pass{
            CGPROGRAM
            
            #pragma vertex Vertex
            #pragma fragment Fragment
            // Make fog work
            #pragma multi_compile_fog

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

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

            sampler2D _MainTexture;
            CBUFFER_START(UnityPerMaterial)
            fixed4 _Colour;
            float4 _MainTexture_ST;
            half _Glossiness;
            half _Metallic;
            CBUFFER_END

            v2f Vertex(appdata input){
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = TRANSFORM_TEX(input.uv, _MainTexture);
                UNITY_TRANSFER_FOG(output, output.vertex);
                return output;
            }

            fixed4 Fragment(v2f input) : SV_Target{
                // Sample the texture
                fixed4 colour = tex2D(_MainTexture, input.uv);
                colour *= _Colour;
                // Apply fog
                UNITY_APPLY_FOG(input.fogCoord, colour);
                return colour;
            }
            ENDCG
        }
    }
    Fallback "Legacy Shaders/Diffuse"
}