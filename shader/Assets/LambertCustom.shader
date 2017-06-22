Shader "Custom/LambertCustom" 
{

	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf LambertCustom
		half4 LightingLambertCustom(SurfaceOutput s, half3 lightDir, half atten)
		{
			half NdotL = dot(lightDir, s.Normal) / 2 + 0.5;

			half4 c;
			c.rgb = (s.Albedo * _LightColor0.rgb * NdotL) * (atten);
			c.a = s.Alpha;

			return c;
		}

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 col = tex2D(_MainTex, IN.uv_MainTex) * _Color;

			o.Albedo = col.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;
			o.Alpha = col.a;
		}
		ENDCG
	}

	FallBack "Diffuse"
}
