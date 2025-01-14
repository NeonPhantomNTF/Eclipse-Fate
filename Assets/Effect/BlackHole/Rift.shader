Shader "Custom/RiftEffect"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" {}
        _Distortion ("Distortion Amount", Range(0, 1)) = 0.5
        _GlowColor ("Glow Color", Color) = (1, 0, 0, 1)
        _TimeSpeed ("Time Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }
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
            };
            
            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            sampler2D _MainTex;
            float _Distortion;
            float _TimeSpeed;
            float4 _GlowColor;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy * 0.5 + 0.5;
                return o;
            }
            
            half4 frag(v2f i) : COLOR
            {
                float2 uv = i.uv;
                uv.x += sin(uv.y * 10.0 + _Time * _TimeSpeed) * _Distortion;
                uv.y += cos(uv.x * 10.0 + _Time * _TimeSpeed) * _Distortion;
                
                half4 texColor = tex2D(_MainTex, uv);
                return texColor * _GlowColor;
            }
            ENDCG
        }
    }
}
