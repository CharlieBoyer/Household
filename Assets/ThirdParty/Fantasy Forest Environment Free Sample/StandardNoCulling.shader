Shader "Custom/FantasyTree"
{
    Properties
    {
        _Cutoff("Mask Clip Value", Range(0, 1)) = 0.5
        _MainTex("Main Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "RenderType"="TransparentCutout"
            "Queue"="Geometry+0"
        }
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha// Default blending mode

        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Cutoff;
            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 tex2DNode3 = tex2D(_MainTex, i.uv);
                clip(tex2DNode3.a - _Cutoff);
                return _Color * tex2DNode3;
            }

            ENDCG
        }
    }

    Fallback "Transparent/Cutout/Diffuse"
}
