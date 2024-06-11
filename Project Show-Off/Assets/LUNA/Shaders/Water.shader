Shader "Luna/Water"{
    Properties{
        _MainWaterColour ("Main Water Colour", Color) = (0.325, 0.5254902, 0.971, 0.725)
        _DepthColourShallow ("Depth Colour Shallow", Color) = (0.325, 0.807, 0.971, 0.725)
        _DepthColourDeep ("Depth Colour Deep", Color) = (0.086, 0.407, 1, 0.749)
        _DepthMaxDistance ("Depth Maximum Distance", Float) = 1
        _SurfaceNoise ("Surface Noise", 2D) = "white" {}
    }
    SubShader{
        Tags{
            "RenderType" = "Opaque"
        }
        LOD 100

        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
            };

            sampler2D _SurfaceNoise;
            sampler2D _CameraDepthTexture;
            CBUFFER_START(UnityPerMaterial)
            float4 _SurfaceNoise_ST;
            float4 _MainWaterColour;
            float4 _DepthColourShallow;
            float4 _DepthColourDeep;
            float _DepthMaxDistance;
            CBUFFER_END

            v2f vert(appdata input){
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = TRANSFORM_TEX(input.uv, _SurfaceNoise);
                UNITY_TRANSFER_FOG(ouput, ouput.vertex);
                output.screenPosition = ComputeScreenPos(output.vertex);
                return output;
            }

            fixed4 frag(v2f input) : SV_Target{
                float existingDepthZeroOne = tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(input.screenPosition)).r;
                float existingDepthLinear = LinearEyeDepth(existingDepthZeroOne);
                float depthDifference = existingDepthLinear - input.screenPosition.w;
                float depthAmount = saturate(depthDifference / _DepthMaxDistance);
                float4 waterColour = lerp(_DepthColourShallow, _DepthColourDeep, depthAmount);
                // Apply fog
                UNITY_APPLY_FOG(input.fogCoord, waterColour);
                return waterColour;
            }
            ENDCG
        }
    }
}