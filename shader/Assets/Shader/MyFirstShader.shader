Shader "Custom/MyFirstShader" 
{
	Properties 
	{
		//_MyColor ("My First Color", Color) = (1,1,1,1)

		_EmissiveColor("Emissive Color", color) = (1, 1, 1, 1)
		_AmbientColor("Ambient Color", color) = (1, 1, 1, 1)
		_PowerValue("Color Power", Range(0, 10)) = 1.5

		//_MainTex ("Albedo (RGB)", 2D) = "white" {}
		//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
		};


		//half _Glossiness;
		//half _Metallic;
		//fixed4 _MyColor;

		float4 _EmissiveColor; //자체색
		float4 _AmbientColor; //환경색
		float _PowerValue;


		void surf (Input IN, inout SurfaceOutput o) 
		{
			// Albedo comes from a texture tinted by color
			//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			float4 c;
			c = pow( (_EmissiveColor + _AmbientColor), _PowerValue);


			o.Albedo = c.rgb;
			//o.Emission = float3(0, 1, 0);

			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;

			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
