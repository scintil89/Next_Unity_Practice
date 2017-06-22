Shader "Custom/MyVertexShader" 
{
		Properties{
			_MainTex1("Tex1", 2D) = "white" {}
			_MainTex2("Tex2", 2D) = "white" {}
			_MainTex3("Tex3", 2D) = "white" {}
			_MainTex4("Tex4", 2D) = "white" {}

			_SpecColor("Specular color", color) = (0.5, 0.5, 0.5, 0)
			_Shiness("Shiness", float) = 1
			_Gloss("Gloss", float) = 1
		}
		
		SubShader{
			Tags{ "RenderType" = "Opaque" } //"Queue" = "Geometry + 1"
			LOD 400

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf BlinnPhong

			// Use shader model 3.0 target, to get nicer looking lighting
			//#pragma target 3.0

		sampler2D _MainTex1;
		sampler2D _MainTex2;
		sampler2D _MainTex3;
		sampler2D _MainTex4;


		float _Shiness;
		float _Gloss;

		struct Input
		{
			float2 uv_MainTex1;
			float2 uv_MainTex2;
			float2 uv_MainTex3;
			float2 uv_MainTex4;

			float4 color : COLOR;
		};


		//half _Glossiness;

		void surf(Input IN, inout SurfaceOutput o) {
			half4 c1 = tex2D(_MainTex1, IN.uv_MainTex1);
			half4 c2 = tex2D(_MainTex2, IN.uv_MainTex2);
			half4 c3 = tex2D(_MainTex3, IN.uv_MainTex3);
			half4 c4 = tex2D(_MainTex4, IN.uv_MainTex4);
			
			float4 finalColor = lerp(c1, c2, IN.color.r);
			finalColor = lerp(finalColor, c3, IN.color.g);
			finalColor = lerp(finalColor, c4, IN.color.b);

			//o.Albedo = finalColor.rgb;
			o.Emission = finalColor;
			o.Alpha = finalColor.a;

			//float3 bumpNormal = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
			//o.Normal = bumpNormal;

			o.Specular = _Shiness;
			o.Gloss = _Gloss;
		}
	
	ENDCG
	}
				FallBack "Diffuse"
}