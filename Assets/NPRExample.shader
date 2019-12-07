Shader "Example/NPRExample" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Outline("Outline", Range(0.0, 1.0)) = 0.01
		_Steps("Steps", Range(0.0, 64.0)) = 4.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			cull front

			CGPROGRAM
			#pragma surface surf Black vertex:vert noshadow
			#pragma target 3.0

			float _Outline;

		void vert(inout appdata_full v)
		{
			v.vertex.xyz += v.normal.xyz * _Outline;
		}

		struct Input {
			float4 color : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) {
		}

		float4 LightingBlack(SurfaceOutput s, float3 lightDir, float atten) {
			return float4(0, 0, 0, 1);
		}

		ENDCG

		cull back

		CGPROGRAM
#pragma surface surf NPR
#pragma target 3.0

		sampler2D _MainTex;
		float _Steps;

		void vert(inout appdata_full v)
		{
			v.vertex.xyz += v.normal.xyz * 0.01;
		}

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		float4 LightingNPR(SurfaceOutput s, float3 lightDir, float atten) {
			float d = dot(s.Normal, lightDir);
			float l = (d + 1.0) * 0.5 * atten;

			float4 final;
			final.rgb = round(s.Albedo *_LightColor0 * l * _Steps) / _Steps;
			final.a = s.Alpha;

			return final;
		}

		ENDCG


	}
	FallBack "Diffuse"
}
