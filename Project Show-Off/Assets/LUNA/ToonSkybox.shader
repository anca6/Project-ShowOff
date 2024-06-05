Shader "Luna/ToonSkybox"{
    Properties{
        [NoScaleOffset] _SunZenithGradient("Sun-Zenith Gradient", 2D) = "white"{}
        [NoSCaleOffset] _ViewZenithGradient("View-Zenith Gradient", 2D) = "white"{}
        [NoScaleOffset] _CloudTexture ("Cloud Texture", 2D) = "white" {}
        [NoScaleOffset] _CloudLocationCubeMap ("Cloud Location Cube Map", Cube) = "black"{}
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
                float3 viewDirectionWorld : TEXCOORD0;
            };
            
            TEXTURE2D(_SunZenithGradient);
            SAMPLER(sampler_SunZenithGradient);

            TEXTURE2D(_ViewZenithGradient);
            SAMPLER(sampler_ViewZenithGradient);

            TEXTURE2D(_CloudTexture);
            SAMPLER(sampler_CloudTexture);

            TEXTURECUBE(_CloudLocationCubeMap);
            SAMPLER(sampler_CloudLocationCubeMap);
            
            float3 _SunDirection;
            
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _SkyColour;
            CBUFFER_END

            v2f vertex(appdata input){
                v2f output;
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.vertex.xyz);
                output.vertex = vertexInput.positionCS;
                output.viewDirectionWorld = vertexInput.positionWS;
                return output;
            }

            float4 fragment(v2f input) : SV_Target{
                float3 viewDirection = normalize(input.viewDirectionWorld);
                
                float viewZenithDot = viewDirection.y;
                float sunZenithDotZeroOne = (_SunDirection.y + 1) * .5;
                
                const float3 sunZenithColour = SAMPLE_TEXTURE2D(_SunZenithGradient, sampler_SunZenithGradient, float2(sunZenithDotZeroOne, .5)).rgb;

                const float3 viewZenithColour = SAMPLE_TEXTURE2D(_ViewZenithGradient, sampler_ViewZenithGradient, float2(sunZenithDotZeroOne, .5)).rgb;
                const float viewZenithMask = pow(saturate(1 - viewZenithDot), 3);

                float2 cloudUV = SAMPLE_TEXTURECUBE(_CloudLocationCubeMap, sampler_CloudLocationCubeMap, viewDirection).rg;
                cloudUV.y = 1 - cloudUV.y;
                const float4 cloudColour = SAMPLE_TEXTURE2D(_CloudTexture, sampler_CloudTexture, cloudUV);
                
                float3 skyColour = sunZenithColour + viewZenithColour * viewZenithMask;
                float4 colour = float4(skyColour, 1) + cloudColour;
                
                return colour;
            }
            ENDHLSL
        }
    }
}