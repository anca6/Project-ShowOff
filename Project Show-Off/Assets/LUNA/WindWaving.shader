Shader "Luna/WindWaving"{
    Properties{
        _MainTex ("Texture", 2D) = "white" {}
        _MainColour ("Main Colour", Color) = (0, 0.5, 0)
        _WorldSize ("World Size", Vector) = (1, 1, 1, 1)
        _WindSpeed ("Wind Speed", Vector) = (1, 1, 1, 1)
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WindAmplitude ("Wind Amplitude", Range(0, 0.5)) = 0.1
        _HeightFactor ("Height Factor", Float) = 1
    }
    SubShader{
        Tags{
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Opaque"
        }
        LOD 100

        Pass{
            Tags{
                "LightMode" = "UniversalForward"
            }
            CGPROGRAM
            #pragma vertex vertex
            #pragma fragment fragment
            // Make fog work
            #pragma multi_compile_fog
            #define PI 3.1415926538

            #include "UnityCG.cginc"

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewDirection : TEXCOORD2;
            };

            sampler2D _MainTex;
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _MainColour;
            float4 _WorldSize;
            float4 _WindSpeed;
            float _WaveSpeed;
            float _WindAmplitude;
            float _HeightFactor;
            CBUFFER_END

            v2f vertex (appdata input){
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                UNITY_TRANSFER_FOG(output, output.vertex);
                float4 worldPosition = mul(unity_ObjectToWorld, input.vertex);
                float2 samplePosition = worldPosition.xz / _WorldSize.xz;
                
                float offset = sin(frac((samplePosition + (_Time.z * _WindSpeed))) * 2 * PI);
                
                output.vertex.x += sin(offset * _WaveSpeed) * _WindAmplitude;
                return output;
            }

            fixed4 fragment (v2f input) : SV_Target{
                const float3 normal = normalize(input.normal);
                
                // Sample the texture
                fixed4 textureColour = tex2D(_MainTex, input.uv);
                // Apply fog
                UNITY_APPLY_FOG(input.fogCoord, colour);
                return _MainColour;
            }
            ENDCG
        }
    }
}