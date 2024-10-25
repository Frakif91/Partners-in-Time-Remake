Shader "Unlit/NewUnlitShader 1"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Scale ("Noise Scale", Float) = 10.5
        _Detail ("Detail Scale", Float) = 11.6
        _Roughness ("Roughness", Float) = 0.492
        _Distortion ("Distortion", Float) = 2.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        float _Scale;
        float _Detail;
        float _Roughness;
        float _Distortion;

        struct Input
        {
            float2 uv_MainTex;
        };

        inline float noise(float2 p) {
            return frac(sin(dot(p, float2(12.9898, 78.233))) * 43758.5453);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex * _Scale;
            float n = noise(uv);

            // Apply detail scaling
            uv *= _Detail;
            n *= noise(uv);

            // Apply distortion
            float dist = sin(uv.x * _Distortion) * sin(uv.y * _Distortion);
            n += dist;

            // Color ramps
            float3 color1 = lerp(float3(0.1, 0.05, 0.02), float3(0.5, 0.25, 0.1), n);
            float3 color2 = lerp(float3(0, 0, 0), float3(1, 1, 1), n);
            
            // Fresnel effect
            float3 viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, float4(o.Normal, 1.0)).xyz);
            float fresnel = pow(1.0 - max(0, dot(viewDir, o.Normal)), 5.0);

            float3 finalColor = lerp(color1, color2, fresnel);
            
            o.Albedo = finalColor;
            o.Metallic = 0.0;
            o.Smoothness = 1.0 - _Roughness;
            o.Alpha = 1.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}