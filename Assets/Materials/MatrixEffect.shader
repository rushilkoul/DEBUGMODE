Shader "CustomRenderTexture/MatrixEffect"
{
    Properties
    {
        _FontAtlas ("Font Atlas (16x16, R)", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _Color ("Matrix Color", Color) = (0.1, 1.0, 0.35, 1)

        _GlyphSize ("Glyph Size (UV Space)", Float) = 0.02
        _Speed ("Rain Speed", Float) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderPipeline"="HDRenderPipeline"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Pass
        {
            Name "MatrixRain"
            Blend One One
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _FontAtlas;
            sampler2D _NoiseTex;

            float4 _Color;
            float _GlyphSize;
            float _Speed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv  = v.uv;
                return o;
            }

            float SampleGlyph(float2 uv)
            {
                float2 fragCoord = uv / _GlyphSize;

                float2 cellUV = frac(fragCoord);
                float2 block  = floor(fragCoord);

                cellUV = cellUV * 0.8 + 0.1;

                float timeStep = floor(_Time.y * 2.0) * 0.01;
                float2 noiseUV = block / float2(256,256) + timeStep;

                float2 glyph = floor(tex2D(_NoiseTex, noiseUV).rg * 16.0);

                float2 atlasUV = (glyph + cellUV) / 16.0;
                atlasUV.y = 1.0 - atlasUV.y;

                return tex2D(_FontAtlas, atlasUV).r;
            }

            float3 RainMask(float2 uv)
            {
                float2 fragCoord = uv / _GlyphSize;

                // lock columns
                fragCoord.x -= fmod(fragCoord.x, 1.0);

                float offset = sin(fragCoord.x * 15.0);
                float speed  = cos(fragCoord.x * 3.0) * 0.3 + 0.7;

                float y = frac(fragCoord.y * 0.1 + _Time.y * speed * _Speed + offset);

                float intensity = smoothstep(1.0, 0.0, y);
                intensity = pow(intensity, 3.0);

                return _Color.rgb * intensity;
            }

            float4 frag (v2f i) : SV_Target
            {
                float glyph = SampleGlyph(i.uv);
                float3 col  = glyph * RainMask(i.uv);

                // i am going to kill myself bro. this brightens the effect a bit
                // I DONT KNOW WHY IT WORKS BUT IT WORKS AND WITHOUT THIS NOTHING WORKS WHAT THE FUCK IS THIS
                col = min(col, 10.0);

                return float4(col, 1);
            }
            ENDHLSL
        }
    }
}
