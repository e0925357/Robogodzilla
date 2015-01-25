//Written by bobthefish, see http://forum.unity3d.com/threads/dissolve-shader.53690/
Shader "Dissolve/Specular" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 0)
	_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
	_MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
	_DissolveTex ("Dissolve (R)", 2D) = "white" {}
}

SubShader {
	Tags {"IgnoreProjector"="True" "RenderType"="TransparentCutout"}
	LOD 300

CGPROGRAM
#pragma surface surf BlinnPhong alphatest:Zero
sampler2D _MainTex;
sampler2D _DissolveTex;
float4 _Color;
float _Shininess;

struct Input {
	float2 uv_MainTex;
	float2 uv_DissolveTex;
};

void surf (Input IN, inout SurfaceOutput o) {
	half4 tex = tex2D(_MainTex, IN.uv_MainTex);
	half4 texd = tex2D(_DissolveTex, IN.uv_DissolveTex);
	o.Albedo = tex.rgb * _Color.rgb;
	o.Gloss = tex.a;
	o.Alpha = _Color.a - texd.r;
	o.Specular = _Shininess;
}
ENDCG
}

Fallback "Transparent/Cutout/VertexLit"
}