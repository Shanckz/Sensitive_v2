// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader_sol"
{
	Properties
	{
		_asset_sol_defaultPolygonShader_BaseColor("asset_sol_defaultPolygonShader_BaseColor", 2D) = "white" {}
		_asset_sol_defaultPolygonShader_Metallic("asset_sol_defaultPolygonShader_Metallic", 2D) = "white" {}
		_asset_sol_defaultPolygonShader_Normal("asset_sol_defaultPolygonShader_Normal", 2D) = "bump" {}
		_asset_sol_defaultPolygonShader_Roughness("asset_sol_defaultPolygonShader_Roughness", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 5)) = 4.160684
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _asset_sol_defaultPolygonShader_Normal;
		uniform float4 _asset_sol_defaultPolygonShader_Normal_ST;
		uniform sampler2D _asset_sol_defaultPolygonShader_BaseColor;
		uniform float4 _asset_sol_defaultPolygonShader_BaseColor_ST;
		uniform sampler2D _asset_sol_defaultPolygonShader_Metallic;
		uniform float4 _asset_sol_defaultPolygonShader_Metallic_ST;
		uniform sampler2D _asset_sol_defaultPolygonShader_Roughness;
		uniform float4 _asset_sol_defaultPolygonShader_Roughness_ST;
		uniform float _Float0;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_asset_sol_defaultPolygonShader_Normal = i.uv_texcoord * _asset_sol_defaultPolygonShader_Normal_ST.xy + _asset_sol_defaultPolygonShader_Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _asset_sol_defaultPolygonShader_Normal, uv_asset_sol_defaultPolygonShader_Normal ) );
			float2 uv_asset_sol_defaultPolygonShader_BaseColor = i.uv_texcoord * _asset_sol_defaultPolygonShader_BaseColor_ST.xy + _asset_sol_defaultPolygonShader_BaseColor_ST.zw;
			o.Albedo = tex2D( _asset_sol_defaultPolygonShader_BaseColor, uv_asset_sol_defaultPolygonShader_BaseColor ).rgb;
			float2 uv_asset_sol_defaultPolygonShader_Metallic = i.uv_texcoord * _asset_sol_defaultPolygonShader_Metallic_ST.xy + _asset_sol_defaultPolygonShader_Metallic_ST.zw;
			o.Metallic = tex2D( _asset_sol_defaultPolygonShader_Metallic, uv_asset_sol_defaultPolygonShader_Metallic ).r;
			float2 uv_asset_sol_defaultPolygonShader_Roughness = i.uv_texcoord * _asset_sol_defaultPolygonShader_Roughness_ST.xy + _asset_sol_defaultPolygonShader_Roughness_ST.zw;
			o.Smoothness = ( tex2D( _asset_sol_defaultPolygonShader_Roughness, uv_asset_sol_defaultPolygonShader_Roughness ) * _Float0 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
893;42;947;990;725.0005;452.3722;1.225;True;True
Node;AmplifyShaderEditor.RangedFloatNode;12;-522.7896,308.7403;Float;False;Property;_Float0;Float 0;4;0;Create;True;0;0;False;0;4.160684;1.662804;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;11;-675.8148,89.87165;Float;True;Property;_asset_sol_defaultPolygonShader_Roughness;asset_sol_defaultPolygonShader_Roughness;3;0;Create;True;0;0;False;0;d5a485a864f13984a9e74c8c31aa4887;d5a485a864f13984a9e74c8c31aa4887;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-137.6292,202.7941;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-625.5072,-113.9637;Float;True;Property;_asset_sol_defaultPolygonShader_Metallic;asset_sol_defaultPolygonShader_Metallic;1;0;Create;True;0;0;False;0;0fba4d19d5c28f749900d789951dc576;0fba4d19d5c28f749900d789951dc576;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-649.6884,573.1195;Float;True;Property;_asset_sol_defaultPolygonShader_Normal;asset_sol_defaultPolygonShader_Normal;2;0;Create;True;0;0;False;0;3d55d1e1db7c0794087196897f82fc5b;6905b35bd517c5247ac411c39ae6b2bd;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-655.5,-304.5;Float;True;Property;_asset_sol_defaultPolygonShader_BaseColor;asset_sol_defaultPolygonShader_BaseColor;0;0;Create;True;0;0;False;0;8e169e9b79d83d042a4171c44f89a33d;8e169e9b79d83d042a4171c44f89a33d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;42,-233;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader_sol;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;11;0
WireConnection;13;1;12;0
WireConnection;0;0;1;0
WireConnection;0;1;3;0
WireConnection;0;3;2;0
WireConnection;0;4;13;0
ASEEND*/
//CHKSM=5624BADE14B4890A111EA7EF8C66B8578B098A1D