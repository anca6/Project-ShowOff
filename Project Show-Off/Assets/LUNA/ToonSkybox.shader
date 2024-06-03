Shader "Luna/ToonSkybox"{
    Properties{
        _MainTex ("Texture", 2D) = "white" {}
        _SkyColour ("Sky Colour", Color) = (0.5, 1, 1, 1)
    }
    SubShader{
        Tags{
            "Queue" = "Background"
            "RenderType" = "Background"
            "PreviewType" = "Skybox"
        }
        Cull Off
        ZWrite Off
        LOD 100

        Pass{
            HLSLPROGRAM
            #pragma vertex vertex
            #pragma fragment fragment

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct appdata{
                float4 vertex : POSITION;
            };

            struct v2f{
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _SkyColour;
            CBUFFER_END

            v2f vertex(appdata input){
                v2f output;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
                output.vertex = vertexInput.positionCS;
                return output;
            }

            float4 fragment(v2f input) : SV_Target{
                
                return _SkyColour;
            }
            ENDHLSL
        }
    }
}