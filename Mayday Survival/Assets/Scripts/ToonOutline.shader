Shader "Custom/ToonOutline"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_AlbedoColor("Albedo Color Adjust", Color) = (1,1,1,1)
		_OutlineColor("Outline Color", Color) = (1,0,0,1)
		_Outline("Outline Width", float) = 2
		_RampTex("Ramp Texture", 2D) = "white" {}
		_EmissiveTex("Texture", 2D) = "black" {}
		_EmissiveColor("Emissive Color", Color) = (1,1,1,1)
		[PerRendererData]_OutlineMultiplication("Multiply Outline", float) = 0
	}
		SubShader
		{
			Tags { "Queue" = "Geometry" }

			//Texture start
			CGPROGRAM
			#pragma surface surf Gwynn fullforwardshadows

			sampler2D _MainTex;
			sampler2D _RampTex;
			sampler2D _EmissiveTex;
			float4 _AlbedoColor;
			float4 _EmissiveColor;

			float4 LightingGwynn(SurfaceOutput s, fixed3 lightDir, fixed atten)
			{
				float diff = dot(s.Normal, lightDir);
				float h = diff * 0.5 * 0.5;
				float2 rh = h;
				float3 ramp = tex2D(_RampTex, rh).rgb;

				float4 c;
				c.rgb = s.Albedo * _LightColor0.rgb * (ramp * 0.7);
				c.a = s.Alpha;
				return c;
			}

			struct Input
			{
				float2 uv_MainTex;
			};

			void surf(Input IN, inout SurfaceOutput o)
			{
				float3 MyAlbedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
				float3 MyEmissive = tex2D(_EmissiveTex, IN.uv_MainTex).rgb;


				o.Albedo.rgb = (MyAlbedo * _AlbedoColor) / 2;
				o.Emission = (MyEmissive * _EmissiveColor) / 2;
			}
			ENDCG
			//Texture end

			//Outline start
			Pass
			{
				Tags{"Queue" = "Transparent"}
				Cull Front

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct v2f
				{
				float4 pos : SV_POSITION;
				};

				float _Outline;
				float4 _OutlineColor;
				float _OutlineMultiplication;

				v2f vert(appdata_base v)
				{
					v2f o;
						v.vertex.xyz += normalize(v.vertex.xyz) * _Outline;
						v.normal *= -1;
						o.pos = UnityObjectToClipPos(v.vertex);

					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					return _OutlineColor * _OutlineMultiplication;
				}
				ENDCG
			}
			//Outline end

			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
		}
}
