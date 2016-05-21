Shader "Custom/DownRes"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		//_Iters ("Iterations", int) = 0
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
			float4 _MainTex_ST;
			uniform int _Iters;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				if (_Iters > 0) 
				{
					uint width = _ScreenParams.x;
					uint height = _ScreenParams.y;
					uint newWidth = width >> _Iters;
					uint newHeight = height >> _Iters;
					uint uvX = i.uv.x * width;
					uint uvY = i.uv.y * height;
					uvX = uvX + pow(2, _Iters - 1);
					uvY = uvY + pow(2, _Iters - 1);
					uvX = uvX >> _Iters;
					uvY = uvY >> _Iters;
					float2 uvNew = float2((float)round(uvX) / newWidth, (float)round(uvY) / newHeight);

					// sample the texture
					fixed4 col = tex2D(_MainTex, uvNew);
					return col;
				}
				else 
				{
					fixed4 col = tex2D(_MainTex, i.uv);
					return col;
				}
			}
			ENDCG
		}
	}
}
