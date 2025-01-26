Shader "Custom/BubbleShader"
{
    Properties
    {
        _MainColor ("Bubble Color", Color) = (1, 1, 1, 1)
        _FresnelPower ("Fresnel Power", Range(0, 10)) = 5
        _ReflectionColor ("Reflection Color", Color) = (1, 1, 1, 1)
        _Distortion ("Distortion Amount", Range(0, 1)) = 0.1
        _NoiseScale ("Noise Scale", Range(1, 10)) = 3
        _NoiseSpeed ("Noise Speed", Range(0, 5)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
            };

            float _FresnelPower;
            float4 _MainColor;
            float4 _ReflectionColor;
            float _Distortion;
            float _NoiseScale;
            float _NoiseSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Fresnel effect
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float fresnel = pow(1.0 - dot(viewDir, normalize(i.worldNormal)), _FresnelPower);

                // Noise for bubble surface
                float2 noiseUV = i.worldPos.xz * _NoiseScale + _Time.y * _NoiseSpeed;
                float noise = sin(noiseUV.x + noiseUV.y) * 0.5 + 0.5;

                // Distortion effect
                float3 distortedNormal = i.worldNormal + noise * _Distortion;

                // Final color blending
                float4 reflectionColor = _ReflectionColor * fresnel;
                float4 baseColor = _MainColor * (1.0 - fresnel);
                float4 finalColor = lerp(baseColor, reflectionColor, fresnel);

                finalColor.a = fresnel; // Adjust alpha based on Fresnel for transparency

                return finalColor;
            }
            ENDCG
        }
    }
}
