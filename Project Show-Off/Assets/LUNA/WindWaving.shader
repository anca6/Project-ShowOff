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
            "RenderType" = "Opaque"
        }
        LOD 100

        Pass{
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
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            CBUFFER_START(UnityPerMaterial)
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

                float heightFactor = pow(input.vertex.y, _HeightFactor);
                
                float offset = sin(frac((samplePosition + (_Time.z * _WindSpeed))) * PI);
                
                output.vertex.z += sin(offset * _WaveSpeed) * _WindAmplitude;
                output.vertex.x += cos(offset * _WaveSpeed) * _WindAmplitude;
                return output;
            }

            fixed4 fragment (v2f input) : SV_Target{
                // Sample the texture
                fixed4 colour = tex2D(_MainTex, input.uv);
                // Apply fog
                UNITY_APPLY_FOG(input.fogCoord, colour);
                return _MainColour;
            }
            ENDCG
        }
    }
}