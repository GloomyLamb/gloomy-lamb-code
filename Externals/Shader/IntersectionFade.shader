Shader "Custom/IntersectionFade_URP"
{
    Properties
    {
        _BaseColor ("Base Color (RGB)", Color) = (1,1,1,1)
        _BaseAlpha ("Base Alpha", Range(0,1)) = 0.7
        _FadeDistance ("Fade Distance", Range(0.001, 5)) = 0.3
        _FadePower ("Fade Power", Range(0.1, 8)) = 1.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalPipeline" }
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

            TEXTURE2D_X(_CameraDepthTexture);
            SAMPLER(sampler_CameraDepthTexture);

            float4 _BaseColor;
            float _BaseAlpha;
            float _FadeDistance;
            float _FadePower;

            struct Attributes
            {
                float3 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 screenPos   : TEXCOORD0;
                float  eyeDepth    : TEXCOORD1;
            };

            Varyings vert(Attributes input)
            {
                Varyings o;

                float3 positionWS = TransformObjectToWorld(input.positionOS);
                o.positionHCS = TransformWorldToHClip(positionWS);

                // screenPos (ComputeScreenPos 대체)
                o.screenPos = ComputeScreenPos(o.positionHCS);

                // view space depth
                float3 positionVS = TransformWorldToView(positionWS);
                o.eyeDepth = -positionVS.z;

                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float2 uv = i.screenPos.xy / i.screenPos.w;

                // URP depth sampling
                float raw = SAMPLE_TEXTURE2D_X(_CameraDepthTexture, sampler_CameraDepthTexture, uv).r;
                float sceneEyeDepth = LinearEyeDepth(raw, _ZBufferParams);

                float diff = sceneEyeDepth - i.eyeDepth;
                float t = saturate(diff / _FadeDistance);
                t = pow(t, _FadePower);

                half4 col = (half4)_BaseColor;
                col.a = (half)(_BaseAlpha * t);
                return col;
            }
            ENDHLSL
        }
    }
}