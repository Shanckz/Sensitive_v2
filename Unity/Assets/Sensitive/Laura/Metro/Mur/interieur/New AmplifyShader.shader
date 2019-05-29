// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Shader_metallic"
{
	Properties
	{
		_asset_murint_fenetre_lambert1_BaseColor("asset_murint_fenetre_lambert1_BaseColor", 2D) = "white" {}
		_asset_murint_fenetre_lambert1_Metallic("asset_murint_fenetre_lambert1_Metallic", 2D) = "white" {}
		_asset_murint_fenetre_lambert1_Normal("asset_murint_fenetre_lambert1_Normal", 2D) = "bump" {}
		_Property_metallic("Property_metallic", Range( 0 , 5)) = 0
		_asset_murint_fenetre_lambert1_Roughness("asset_murint_fenetre_lambert1_Roughness", 2D) = "white" {}
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

		uniform sampler2D _asset_murint_fenetre_lambert1_Normal;
		uniform float4 _asset_murint_fenetre_lambert1_Normal_ST;
		uniform sampler2D _asset_murint_fenetre_lambert1_BaseColor;
		uniform float4 _asset_murint_fenetre_lambert1_BaseColor_ST;
		uniform sampler2D _asset_murint_fenetre_lambert1_Metallic;
		uniform float4 _asset_murint_fenetre_lambert1_Metallic_ST;
		uniform float _Property_metallic;
		uniform sampler2D _asset_murint_fenetre_lambert1_Roughness;
		uniform float4 _asset_murint_fenetre_lambert1_Roughness_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_asset_murint_fenetre_lambert1_Normal = i.uv_texcoord * _asset_murint_fenetre_lambert1_Normal_ST.xy + _asset_murint_fenetre_lambert1_Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _asset_murint_fenetre_lambert1_Normal, uv_asset_murint_fenetre_lambert1_Normal ) );
			float2 uv_asset_murint_fenetre_lambert1_BaseColor = i.uv_texcoord * _asset_murint_fenetre_lambert1_BaseColor_ST.xy + _asset_murint_fenetre_lambert1_BaseColor_ST.zw;
			o.Albedo = tex2D( _asset_murint_fenetre_lambert1_BaseColor, uv_asset_murint_fenetre_lambert1_BaseColor ).rgb;
			float2 uv_asset_murint_fenetre_lambert1_Metallic = i.uv_texcoord * _asset_murint_fenetre_lambert1_Metallic_ST.xy + _asset_murint_fenetre_lambert1_Metallic_ST.zw;
			o.Metallic = ( tex2D( _asset_murint_fenetre_lambert1_Metallic, uv_asset_murint_fenetre_lambert1_Metallic ) * _Property_metallic ).r;
			float2 uv_asset_murint_fenetre_lambert1_Roughness = i.uv_texcoord * _asset_murint_fenetre_lambert1_Roughness_ST.xy + _asset_murint_fenetre_lambert1_Roughness_ST.zw;
			o.Smoothness = tex2D( _asset_murint_fenetre_lambert1_Roughness, uv_asset_murint_fenetre_lambert1_Roughness ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
398;44;947;966;1176.264;652.8481;2.068044;True;True
Node;AmplifyShaderEditor.SamplerNode;2;-532.527,129.1755;Float;True;Property;_asset_murint_fenetre_lambert1_Metallic;asset_murint_fenetre_lambert1_Metallic;1;0;Create;True;0;0;False;0;1a78e5fe6cf16e9408ea311c03053149;1a78e5fe6cf16e9408ea311c03053149;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-456.4238,329.8584;Float;False;Property;_Property_metallic;Property_metallic;3;0;Create;True;0;0;False;0;0;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-447.5,-185;Float;True;Property;_asset_murint_fenetre_lambert1_BaseColor;asset_murint_fenetre_lambert1_BaseColor;0;0;Create;True;0;0;False;0;8a4fdf4d215908e41a76d7f67c921a22;8a4fdf4d215908e41a76d7f67c921a22;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-795.5475,28.25774;Float;True;Property;_asset_murint_fenetre_lambert1_Normal;asset_murint_fenetre_lambert1_Normal;2;0;Create;True;0;0;False;0;81a716f48b0d1e342bdca9b597f925ab;81a716f48b0d1e342bdca9b597f925ab;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-388.885,493.2468;Float;True;Property;_asset_murint_fenetre_lambert1_Roughness;asset_murint_fenetre_lambert1_Roughness;4;0;Create;True;0;0;False;0;b9dffea22e0b0e441945bd54137a5757;b9dffea22e0b0e441945bd54137a5757;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-79.70876,182.8421;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;159.1981,-105.4703;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Shader_metallic;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;2;0
WireConnection;5;1;4;0
WireConnection;0;0;1;0
WireConnection;0;1;3;0
WireConnection;0;3;5;0
WireConnection;0;4;6;0
ASEEND*/
//CHKSM=665ACF2803A7EEB0F97496BC7663E71896D41463