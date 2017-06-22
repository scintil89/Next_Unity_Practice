Shader "Custom/MySpecularShader" 
{
	Properties
	{
		//_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_SpecColor("Specular color", color) = (0.5, 0.5, 0.5, 0)
		_Shiness("Shiness", float) = 1
		_Gloss("Gloss", float) = 1
	//_Glossiness ("Smoothness", Range(0,1)) = 0.5
		//_Metallic ("Metallic", Range(0,1)) = 0.0
	}
		
		SubShader{
		Tags{ "RenderType" = "Opaque" } //"Queue" = "Geometry + 1"
		LOD 400

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf BlinnPhong

		// Use shader model 3.0 target, to get nicer looking lighting
		//#pragma target 3.0

		sampler2D _MainTex;
		float _Shiness;
		float _Gloss;

	struct Input
	{
		float2 uv_MainTex;
	};

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;

	void surf(Input IN, inout SurfaceOutput o) {
		// Albedo comes from a texture tinted by color
		//fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

		half4 c = tex2D(_MainTex, IN.uv_MainTex);

		o.Albedo = c.rgb;
		// Metallic and smoothness come from slider variables
		//o.Metallic = _Metallic;
		//o.Smoothness = _Glossiness;
		o.Alpha = c.a;

		o.Specular = _Shiness;
		o.Gloss = _Gloss;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
