Shader "Custom/CartoonShader" {
	Properties
	{
		_Amount("Outline", float) = 0.03
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		cull front

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert //vertex:vert 붙혀야함

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		struct Input 
		{
			float4 color : COLOR;
		};

		float _Amount;

		void vert(inout appdata_full v)
		{
			v.vertex.xyz += v.normal * _Amount;
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{

		}
		ENDCG
	}
	FallBack "Diffuse"
}
