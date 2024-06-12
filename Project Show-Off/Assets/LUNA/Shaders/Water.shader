Shader "Luna/Water"{
    Properties{
        _MainWaterColour ("Main Water Colour", Color) = (0.325, 0.5254902, 0.971, 0.725)
        _DepthColourShallow ("Depth Colour Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
        _DepthColourDeep ("Depth Colour Deep", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance ("Depth Maximum Distance", Float) = 1
        _SurfaceNoise ("Surface Noise", 2D) = "white" {}
        _SurfaceNoiseCutoff ("Surface Noise Cutoff", Range(0, 1)) = 0.7
        _FoamDistance ("Foam Distance", Float) = 0.2
        _SurfaceDistortion ("Surface Distortion Texture", 2D) = "white" {}
    }
    SubShader{
        Tags{
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
        }
        LOD 100

        Pass{
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            CGPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment
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
                float4 screenPosition : TEXCOORD2;
                float2 distortUV : TEXCOORD3;
            };

            sampler2D _SurfaceNoise;
            sampler2D _CameraDepthTexture;
            sampler2D _SurfaceDistortion;
            CBUFFER_START(UnityPerMaterial)
            float4 _SurfaceNoise_ST;
            float4 _MainWaterColour;
            float4 _DepthColourShallow;
            float4 _DepthColourDeep;
            float _DepthMaxDistance;
            float _SurfaceNoiseCutoff;
            float _FoamDistance;
            float4 _SurfaceDistortion_ST;
            CBUFFER_END

            v2f Vertex(appdata input){
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = TRANSFORM_TEX(input.uv, _SurfaceNoise);
                UNITY_TRANSFER_FOG(ouput, ouput.vertex);
                output.screenPosition = ComputeScreenPos(output.vertex);
                output.distortUV = TRANSFORM_TEX(input.uv, _SurfaceDistortion);
                return output;
            }

            fixed4 Fragment(v2f input) : SV_Target{
                const float existingDepthZeroOne = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(input.screenPosition)).r;
                const float existingDepthLinear = LinearEyeDepth(existingDepthZeroOne);
                const float depthDifference = existingDepthLinear - input.screenPosition.w;
                const float depthAmount = saturate(depthDifference / _DepthMaxDistance);
                const float4 waterColour = lerp(_DepthColourShallow, _DepthColourDeep, depthAmount);

                const float foamDepthDifference = saturate(depthDifference / _FoamDistance);
                const float surfaceNoiseCutoff = foamDepthDifference * _SurfaceNoiseCutoff;
                
                const float4 surfaceNoiseSample = tex2D(_SurfaceNoise, input.uv);
                const float surfaceNoiseColour = step(surfaceNoiseCutoff, surfaceNoiseSample);
                
                float4 colour = waterColour + surfaceNoiseColour;
                
                // Apply fog
                UNITY_APPLY_FOG(input.fogCoord, colour);
                return colour;
            }
            ENDCG
        }
    }
}