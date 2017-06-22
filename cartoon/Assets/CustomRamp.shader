Shader "Custom/CustomRamp" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_RampTex("Ramp", 2D) = "white" {}
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf CustomRamp 
		sampler2D _MainTex;
		sampler2D _RampTex; //
		
		half4 LightingCustomRamp(SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL = dot(s.Normal, lightDir) * 0.5 + 0.5;
			float4 ramp = tex2D(_RampTex, float2(NdotL, 0));

			half4 c;
			c.rgb = s.Albedo * _LightColor0.rgb * (ramp.rgb * atten);

			c.a = s.Alpha;

			return c;
		}

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

	
		struct Input
		{
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D(_MainTex, float2(IN.uv_MainTex.x, IN.uv_MainTex.y + _Time.x * 3));

			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
