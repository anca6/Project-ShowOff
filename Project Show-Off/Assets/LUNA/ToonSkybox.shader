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
            CGPROGRAM
            #pragma vertex vertex
            #pragma fragment fragment

            #include "UnityCG.cginc"

            struct appdata{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            CBUFFER_START(UnityPerMaterial)
            float4 _MainTex_ST;
            float4 _SkyColour;
            CBUFFER_END

            v2f vertex(appdata input){
                v2f output;
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return output;
            }

            fixed4 fragment(v2f input) : SV_Target{
                // Sample the texture
                fixed4 colour = tex2D(_MainTex, input.uv);
                
                return _SkyColour;
            }
            ENDCG
        }
    }
}