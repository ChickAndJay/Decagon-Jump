Shader "Custom/HorizontalGradiantSky" {
	Properties {
		_Color1("Top Color", Color) = (1,1,1,1)
		_Color ("Bottom Color", Color) = (1,1,1,1)
				_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0

		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard Lambert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		fixed4 _Color1;
		half _Glossiness;
		half _Metallic;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};


		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * lerp(_Color, _Color1, IN.uv_MainTex.y/0.7);
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;

			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Vertex"
}
