Shader "Custom/CameraBlur"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_BlurTex("Blur", 2D) = "white" {}
		_Magnitude("Magnitude", Range(0, 1)) = 0
		_BlurSpeed("BlurSpeed", Range(0, 1)) = 0.15
		_Tint("Tint", Color) = (1, 1, 1, 1)
		_TintSpeed("TintSpeed", Range(0, 20)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
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
			sampler2D _BlurTex;
			uniform float _GlobalMagnitude;
			float _BlurSpeed;
			float4 _MainTex_ST;
			float4 _Tint;
			float _TintSpeed;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float2 displacement = tex2D(_BlurTex, i.uv + (_Time.y * _BlurSpeed)).xy;
				displacement = ((displacement * 2) - 1) * _GlobalMagnitude;

				fixed4 col = tex2D(_MainTex, i.uv + displacement);

				float tween = ((sin(_Time.y * _TintSpeed) + 1) / 2) * _Tint.a;
				col.rgb = (col.rgb * (1.0 - tween)) + (_Tint.rgb * tween);
				return col;
			}
			ENDCG
		}
	}
}
