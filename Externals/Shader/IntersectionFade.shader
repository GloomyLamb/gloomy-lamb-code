Shader "Custom/IntersectionFade_URP"
{
     Properties
    {
        _BaseMap ("Base Texture", 2D) = "white" {}
        _BaseColor ("Base Color (RGB)", Color) = (0,0,0,1)
        _BaseAlpha ("Base Alpha", Range(0,1)) = 0.7

        _FadeDistance ("Fade Distance", Range(0.001, 5)) = 0.3
        _FadePower ("Fade Power", Range(0.1, 8)) = 1.0

        _MinLuminance ("Min Luminance", Range(0,0.5)) = 0.15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "RenderPipeline"="UniversalPipeline"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        ZTest LEqual
        Cull Back

        Pass
        {
            Name "Forward"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            TEXTURE2D_X(_CameraDepthTexture);
            SAMPLER(sampler_CameraDepthTexture);

            float4 _BaseColor;
            float  _BaseAlpha;
            float  _FadeDistance;
            float  _FadePower;
            float  _MinLuminance;

            struct Attributes
            {
                float3 positionOS : POSITION;
                float2 uv         : TEXCOORD0;
                float4 color      : COLOR;        // ✅ 파티클 컬러
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 screenPos   : TEXCOORD0;
                float2 uv          : TEXCOORD1;
                float4 color       : COLOR;        // ✅ 전달
                float  eyeDepth    : TEXCOORD2;
            };

            Varyings vert (Attributes input)
            {
                Varyings o;

                float3 positionWS = TransformObjectToWorld(input.positionOS);
                o.positionHCS = TransformWorldToHClip(positionWS);
                o.screenPos   = ComputeScreenPos(o.positionHCS);
                o.uv          = input.uv;
                o.color       = input.color;      // ✅ 그대로 넘김

                float3 positionVS = TransformWorldToView(positionWS);
                o.eyeDepth = -positionVS.z;

                return o;
            }

            half4 frag (Varyings i) : SV_Target
            {
                float2 uvScreen = i.screenPos.xy / i.screenPos.w;

                float rawDepth = SAMPLE_TEXTURE2D_X(
                    _CameraDepthTexture,
                    sampler_CameraDepthTexture,
                    uvScreen
                ).r;

                float sceneEyeDepth = LinearEyeDepth(rawDepth, _ZBufferParams);

                float diff = sceneEyeDepth - i.eyeDepth;
                float t = saturate(diff / _FadeDistance);
                t = pow(t, _FadePower);

                half4 tex = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv);

                float luminance = max(t, _MinLuminance);

                half4 col;
                col.rgb = tex.rgb * _BaseColor.rgb * luminance * i.color.rgb;
                col.a   = tex.a * _BaseAlpha * t * i.color.a; // ✅ 여기서 먹음

                return col;
            }
            ENDHLSL
        }
    }
}