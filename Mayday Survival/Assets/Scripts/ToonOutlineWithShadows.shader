Shader "Custom/ToonOutlineWithShadows"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (1,0,0,1)
		_Outline("Outline Width", float) = 2
		_EmissiveTex("Texture", 2D) = "black" {}
		_EmissiveColor("Emissive Color", Color) = (1,1,1,1)
		[PerRendererData]_OutlineMultiplication("Multiply Outline", float) = 0
	}
		SubShader
		{
			Pass
			{
			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				TRANSFER_SHADOW(o)
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float3 viewDir = normalize(i.viewDir);


				// Calculate illumination from directional light.
				float NdotL = dot(_WorldSpaceLightPos0, normal);

				// Samples the shadow map, returning a value in the 0...1 range,
				float shadow = SHADOW_ATTENUATION(i);
				float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
				// Multiply by the main directional light's intensity and color.
				lightIntensity = lightIntensity > 0.1 ? lightIntensity / 4 : lightIntensity;
				float4 light = lightIntensity * _LightColor0;

				float4 sample = tex2D(_MainTex, i.uv);

				return light * sample;
			}
			ENDCG
		}

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
