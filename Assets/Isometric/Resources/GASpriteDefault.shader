Shader "Custom/GASpriteDefault" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff ("Cutout", Range(0, 1)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Cull Off

		CGPROGRAM
		#pragma surface surf GASprite addshadow alphatest:_Cutoff
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 color : COLOR;
		};

		fixed4 _Color;

		float4 LightingGASprite(SurfaceOutput s, float3 lightDir, float atten)
		{
			float4 col;

			col.rgb = s.Albedo * atten * _LightColor0.rgb;
			col.a = s.Alpha;

			return col;
		}

		void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb * IN.color;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Transparent/Cutout/Diffuse"
}
