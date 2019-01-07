Shader "Hidden/FadeBorder"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			
			// Width of the border in pixels
			fixed _borderWidth;

			float linearStep(float a, float b, float t)
			{
				return saturate((t - a) / (b - a));
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				// Distance to border along x and y
				float2 distanceInPixels = (0.5 - abs(i.uv.xy - 0.5)) * _ScreenParams.xy;
				// Linear border fade
				float mask = linearStep(0, _borderWidth, min(distanceInPixels.x, distanceInPixels.y));

				//return col + lerp(fixed4(1, 0, 0, 1), fixed4(0, 1, 0, 1), mask);

				// Return masked color
				return col * mask;
			}
			ENDCG
		}
	}
}
