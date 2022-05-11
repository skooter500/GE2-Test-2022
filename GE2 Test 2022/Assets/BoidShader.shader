// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Boid" {
	Properties {
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_PositionScale("PositionScale", Range(0, 100000)) = 250
		_TimeMultiplier("Time Scale", Range(0, 100)) = 1
		_Alpha("Alpha", Range(0, 100)) = 1
		_Offset("Offset", Range(0, 100000)) = 0
		_CI("CI", Range(0, 10000000)) = 0
		_ColorStart("ColorStart", Range(0, 1)) = 0
		_ColorEnd("ColorEnd", Range(0, 1)) = 1
		_ColorShift("ColorShift", Range(-1, 1)) = 1
		
	}
	SubShader {
		Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade
         
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		
		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		half _Alpha;
		half _Offset;

		float _PositionScale;
		float _TimeMultiplier;
		float _ColorStart;
		float _ColorEnd;
		float _ColorShift;
		
		float _CI;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		float map(float value, float r1, float r2, float m1, float m2)
    	{
        float dist = value - r1;
        float range1 = r2 - r1;
        float range2 = m2 - m1;
        return m1 + ((dist / range1) * range2);
    	}
	
		float3 hsv_to_rgb(float3 HSV)
		{
			float3 RGB = HSV.z;

			float var_h = HSV.x * 6;
			float var_i = floor(var_h);   // Or ... var_i = floor( var_h )
			float var_1 = HSV.z * (1.0 - HSV.y);
			float var_2 = HSV.z * (1.0 - HSV.y * (var_h - var_i));
			float var_3 = HSV.z * (1.0 - HSV.y * (1 - (var_h - var_i)));
			if (var_i == 0) { RGB = float3(HSV.z, var_3, var_1); }
			else if (var_i == 1) { RGB = float3(var_2, HSV.z, var_1); }
			else if (var_i == 2) { RGB = float3(var_1, HSV.z, var_3); }
			else if (var_i == 3) { RGB = float3(var_1, var_2, HSV.z); }
			else if (var_i == 4) { RGB = float3(var_3, var_1, HSV.z); }
			else { RGB = float3(HSV.z, var_1, var_2); }

			return (RGB);
		}

		float pingpongMap(float a, float b, float c, float d, float e)
		{
		float range1 = c - b;
		float range2 = e-d;
		if (range1 == 0)
		{
			return 0;
		}
		
		float howFar = a - b;
		
		float howMany = floor(howFar / range1);
		float fraction = (howFar - (howMany * range1)) / range1;
		//println(a + " howMany" + howMany + " fraction: " + fraction);
		//println(range2 + " " + fraction);
		if (howMany % 2 == 0)
		{
			return d + (fraction * range2);
		}
		else
		{
			return e - (fraction * range2);
		}
		
		
		}
		

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
						
			float d = length(IN.worldPos);
			
			float f = _Time * _TimeMultiplier;

			float cs = _ColorStart;
			
			float ce = _ColorEnd;			
			
			float hue = pingpongMap(d + f, 0, _PositionScale, cs, ce) + _ColorShift;
			if (hue > 1.0f)
			{
				hue = hue - 1.0f;
			}
			if (hue < 0)
			{
				hue = 1.0f - hue;
			}
			
			float b = 1; // map(d, 0, 200, 2, 1);
			
			float camD = length(_WorldSpaceCameraPos);
			
			/*float marr[] = {1,1,5,50,2000,200000, 50000000, 700000000};
			float i = camD / 40.0;
			float range = marr[i + 1]  - marr[i];  			
			float m = (marr[i]) + ((i - (int) i) * range);
			*/
			
			//float ci = 1 + pow(_CI, 1.0 / d);
			float ci = 1 + (_CI *  (1.0 / d));//pow(_CI, 1.0 / d);
			fixed3 c = hsv_to_rgb(float3(hue, 1, b * ci));
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

/*
Shader "Custom/CreatureColours" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_PositionScale("PositionScale", Range(0, 1000)) = 250
		_Fade("Fade", Range(0, 1)) = 1
	}
	SubShader {
		Tags {"Queue" = "Transparent" "RenderType"="Transparent" }
		LOD 200

		ZWrite On
		Cull Back
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask RGB
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		half _Fade;

		float _PositionScale;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float u,v;			
			u = abs(IN.worldPos.x / _PositionScale);
			//u -= (int)u;
            v = abs(IN.worldPos.z / _PositionScale);
			//v -= (int)v;
			fixed4 c = tex2D (_MainTex, float2(u,v));
			o.Albedo = c.rgb;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = _Fade;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
*/