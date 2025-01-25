Shader "Custom/CartoonWater"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" { }
        _NormalTex("Normal Map", 2D) = "bump" { }
        _WaveStrength("Wave Strength", Range(0.0, 1.0)) = 0.5
        _FoamTex("Foam Texture", 2D) = "white" { }
        _FoamStrength("Foam Strength", Range(0.0, 1.0)) = 0.5
        _Shininess("Shininess", Range(0.0, 1.0)) = 0.3
        _WaveSpeed("Wave Speed", Range(0.0, 1.0)) = 0.1
    }

        SubShader
        {
            Tags { "Queue" = "Background" }
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float3 worldPos : TEXCOORD1;
                };

                uniform float _WaveStrength;
                uniform float _WaveSpeed;
                uniform float _FoamStrength;

                sampler2D _MainTex;
                sampler2D _NormalTex;
                sampler2D _FoamTex;

                float4 _MainTex_ST;
                float4 _NormalTex_ST;
                float4 _FoamTex_ST;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                    // 波動效果：增加頻率並且使波動更加順暢
                    float wave = sin(o.worldPos.x * 0.2 + _Time.y * _WaveSpeed * 5.0) * sin(o.worldPos.z * 0.2 + _Time.y * _WaveSpeed * 5.0) * _WaveStrength;
                    o.pos.y += wave; // 將波動應用於頂點位置

                    return o;
                }

                half4 frag(v2f i) : SV_Target
                {
                    // 來自基底材質的顏色
                    half4 col = tex2D(_MainTex, i.uv);

                    // 基於高度的泡沫添加（簡單的近似）
                    float foam = tex2D(_FoamTex, i.uv).r * _FoamStrength;
                    col += foam;

                    // 添加法線貼圖效果（模擬波動）
                    half3 normal = tex2D(_NormalTex, i.uv).rgb;
                    normal = normalize(normal * 2.0 - 1.0); // 將法線從[0,1]轉換到[-1,1]
                    col.rgb += normal * 0.1; // 來自法線的輕微光照效果

                    return col;
                }
                ENDCG
            }
        }
            Fallback "Diffuse"
}
