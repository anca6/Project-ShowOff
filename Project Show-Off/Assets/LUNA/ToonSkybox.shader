Shader "Luna/ToonSkybox"{
    Properties{
        [NoScaleOffset] _SunZenithGradient("Sun-Zenith Gradient", 2D) = "white"{}
        [NoSCaleOffset] _ViewZenithGradient("View-Zenith Gradient", 2D) = "white"{}
        [NoScaleOffset] _CloudCubeMap ("Cloud Cube Map", Cube) = "black"{}
        _CloudRotation ("Cloud Rotation", Range(0, 1)) = 0
        _CloudSpeed ("Cloud Speed", Range(0, 1)) = 0.15
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

            TEXTURECUBE(_CloudCubeMap);
            SAMPLER(sampler_CloudCubeMap);
            
            float3 _SunDirection;
            
            CBUFFER_START(UnityPerMaterial)
            float _CloudRotation;
            float _CloudSpeed;
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

                float cloudRotationRadians = (_CloudRotation + _Time.x * _CloudSpeed * .25) * TWO_PI;

                float3x3 cloudRotationMatrix = float3x3(
                    cos(cloudRotationRadians), 0, sin(cloudRotationRadians),
                    0, 1, 0,
                    -sin(cloudRotationRadians), 0, cos(cloudRotationRadians)
                );
                
                const float3 cloudDirection = mul(cloudRotationMatrix, viewDirection);
                const float4 cloudColour = SAMPLE_TEXTURECUBE(_CloudCubeMap, sampler_CloudCubeMap, cloudDirection);
                
                float3 skyColour = sunZenithColour + viewZenithColour * viewZenithMask;
                float4 colour = (float4(skyColour, 1) * (1 - cloudColour.a)) + cloudColour * cloudColour.a;
                
                return colour;
            }
            ENDHLSL
        }
    }
}