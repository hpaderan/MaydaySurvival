Shader "Custom/ToonGround"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_AlbedoColor("Albedo Color Adjust", Color) = (1,1,1,1)
		_RampTex("Ramp Texture", 2D) = "white" {}
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
			UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
		}
}
