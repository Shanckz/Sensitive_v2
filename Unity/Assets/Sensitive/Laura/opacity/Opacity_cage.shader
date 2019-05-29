// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Transparent"
{
	Properties
	{
		_asset_mur_cage_lambert1_Emissive("asset_mur_cage_lambert1_Emissive", 2D) = "white" {}
		_Cutoff( "Mask Clip Value", Float ) = 1
		_asset_mur_cage_lambert1_Normal("asset_mur_cage_lambert1_Normal", 2D) = "bump" {}
		_opacity1("opacity 1", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _asset_mur_cage_lambert1_Normal;
		uniform float4 _asset_mur_cage_lambert1_Normal_ST;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _asset_mur_cage_lambert1_Emissive;
		uniform float4 _asset_mur_cage_lambert1_Emissive_ST;
		uniform sampler2D _opacity1;
		uniform float4 _opacity1_ST;
		uniform float _Cutoff = 1;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_asset_mur_cage_lambert1_Normal = i.uv_texcoord * _asset_mur_cage_lambert1_Normal_ST.xy + _asset_mur_cage_lambert1_Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _asset_mur_cage_lambert1_Normal, uv_asset_mur_cage_lambert1_Normal ) );
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Albedo = tex2D( _TextureSample0, uv_TextureSample0 ).rgb;
			float2 uv_asset_mur_cage_lambert1_Emissive = i.uv_texcoord * _asset_mur_cage_lambert1_Emissive_ST.xy + _asset_mur_cage_lambert1_Emissive_ST.zw;
			o.Emission = tex2D( _asset_mur_cage_lambert1_Emissive, uv_asset_mur_cage_lambert1_Emissive ).rgb;
			o.Alpha = 1;
			float2 uv_opacity1 = i.uv_texcoord * _opacity1_ST.xy + _opacity1_ST.zw;
			clip( tex2D( _opacity1, uv_opacity1 ).a - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
531;21;1287;951;1521.591;487.0027;1.783856;True;True
Node;AmplifyShaderEditor.SamplerNode;19;-682.5834,-229.9942;Float;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;edadef7633991f941af2a9e981cf7e83;edadef7633991f941af2a9e981cf7e83;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;25;-629.5061,2.579613;Float;True;Property;_opacity1;opacity 1;5;0;Create;True;0;0;False;0;9edb6c2f3fbb3104ea52873634890606;9edb6c2f3fbb3104ea52873634890606;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;17;-853.0836,211.4217;Float;True;Property;_asset_mur_cage_lambert1_Roughness;asset_mur_cage_lambert1_Roughness;4;0;Create;True;0;0;False;0;17cdfab71f070ee439e637e8d7623c49;17cdfab71f070ee439e637e8d7623c49;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-827.4737,409.2183;Float;True;Property;_asset_mur_cage_lambert1_Emissive;asset_mur_cage_lambert1_Emissive;1;0;Create;True;0;0;False;0;437ac5c29ab1faf4ea1fb9539751830d;437ac5c29ab1faf4ea1fb9539751830d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-804.3729,617.5952;Float;True;Property;_asset_mur_cage_lambert1_Normal;asset_mur_cage_lambert1_Normal;3;0;Create;True;0;0;False;0;ea2d036ebbd870443b6f3a4ab34736cc;30bfb97f533c81b40b9f4b9da1e1af95;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;104.5954,43.50293;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Transparent;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;1;True;True;0;False;TransparentCutout;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;2;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;19;0
WireConnection;0;1;16;0
WireConnection;0;2;15;0
WireConnection;0;10;25;4
ASEEND*/
//CHKSM=D2D048415D7D6B87AFB6DEE405B0C4163F63A00C