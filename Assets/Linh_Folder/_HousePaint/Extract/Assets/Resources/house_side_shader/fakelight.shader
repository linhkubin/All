Shader "Unlit/FakeLight" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_ColorOverlay ("Color Overlay", Vector) = (1,1,1,1)
		_Multiply ("Multiply color", Float) = 1
		_LightGradient ("Light Gradient", 2D) = "white" {}
		_NormalScale ("Normals Scale", Vector) = (1.25,1.75,1.5,1)
	}
	SubShader {
		LOD 100
		Tags { "RenderType" = "Opaque" }
		Pass {
			LOD 100
			Tags { "RenderType" = "Opaque" }
			GpuProgramID 36052
			Program "vp" {
				SubProgram "gles hw_tier00 " {
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	vec4 _MainTex_ST;
					attribute highp vec4 in_POSITION0;
					attribute highp vec2 in_TEXCOORD0;
					attribute highp vec3 in_NORMAL0;
					varying highp vec2 vs_TEXCOORD0;
					varying highp vec3 vs_NORMAL0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = dot(hlslcc_mtx4x4unity_ObjectToWorld[0].xyz, hlslcc_mtx4x4unity_ObjectToWorld[0].xyz);
					    u_xlat0.w = dot(hlslcc_mtx4x4unity_ObjectToWorld[1].xyz, hlslcc_mtx4x4unity_ObjectToWorld[1].xyz);
					    u_xlat0.xy = sqrt(u_xlat0.xw);
					    u_xlat6 = dot(hlslcc_mtx4x4unity_ObjectToWorld[2].xyz, hlslcc_mtx4x4unity_ObjectToWorld[2].xyz);
					    u_xlat0.z = sqrt(u_xlat6);
					    u_xlat0.xyz = in_NORMAL0.xyz / u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    vs_NORMAL0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					uniform 	vec4 _ColorOverlay;
					uniform 	vec4 _NormalScale;
					uniform 	float _Multiply;
					uniform lowp sampler2D _MainTex;
					uniform lowp sampler2D _LightGradient;
					varying highp vec2 vs_TEXCOORD0;
					varying highp vec3 vs_NORMAL0;
					#define SV_Target0 gl_FragData[0]
					vec4 u_xlat0;
					mediump vec4 u_xlat16_0;
					lowp vec4 u_xlat10_0;
					vec4 u_xlat1;
					lowp vec4 u_xlat10_1;
					void main()
					{
					    u_xlat0.xyz = vs_NORMAL0.xyz * _NormalScale.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
					    u_xlat0.x = u_xlat0.x * 0.333333343;
					    u_xlat10_0 = texture2D(_LightGradient, u_xlat0.xx);
					    u_xlat10_1 = texture2D(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat10_1 * _ColorOverlay;
					    u_xlat16_0 = u_xlat10_0 * u_xlat1;
					    u_xlat0 = u_xlat16_0 * vec4(_Multiply);
					    SV_Target0 = u_xlat0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles hw_tier01 " {
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	vec4 _MainTex_ST;
					attribute highp vec4 in_POSITION0;
					attribute highp vec2 in_TEXCOORD0;
					attribute highp vec3 in_NORMAL0;
					varying highp vec2 vs_TEXCOORD0;
					varying highp vec3 vs_NORMAL0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = dot(hlslcc_mtx4x4unity_ObjectToWorld[0].xyz, hlslcc_mtx4x4unity_ObjectToWorld[0].xyz);
					    u_xlat0.w = dot(hlslcc_mtx4x4unity_ObjectToWorld[1].xyz, hlslcc_mtx4x4unity_ObjectToWorld[1].xyz);
					    u_xlat0.xy = sqrt(u_xlat0.xw);
					    u_xlat6 = dot(hlslcc_mtx4x4unity_ObjectToWorld[2].xyz, hlslcc_mtx4x4unity_ObjectToWorld[2].xyz);
					    u_xlat0.z = sqrt(u_xlat6);
					    u_xlat0.xyz = in_NORMAL0.xyz / u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    vs_NORMAL0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					uniform 	vec4 _ColorOverlay;
					uniform 	vec4 _NormalScale;
					uniform 	float _Multiply;
					uniform lowp sampler2D _MainTex;
					uniform lowp sampler2D _LightGradient;
					varying highp vec2 vs_TEXCOORD0;
					varying highp vec3 vs_NORMAL0;
					#define SV_Target0 gl_FragData[0]
					vec4 u_xlat0;
					mediump vec4 u_xlat16_0;
					lowp vec4 u_xlat10_0;
					vec4 u_xlat1;
					lowp vec4 u_xlat10_1;
					void main()
					{
					    u_xlat0.xyz = vs_NORMAL0.xyz * _NormalScale.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
					    u_xlat0.x = u_xlat0.x * 0.333333343;
					    u_xlat10_0 = texture2D(_LightGradient, u_xlat0.xx);
					    u_xlat10_1 = texture2D(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat10_1 * _ColorOverlay;
					    u_xlat16_0 = u_xlat10_0 * u_xlat1;
					    u_xlat0 = u_xlat16_0 * vec4(_Multiply);
					    SV_Target0 = u_xlat0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles hw_tier02 " {
					"!!GLES
					#ifdef VERTEX
					#version 100
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	vec4 _MainTex_ST;
					attribute highp vec4 in_POSITION0;
					attribute highp vec2 in_TEXCOORD0;
					attribute highp vec3 in_NORMAL0;
					varying highp vec2 vs_TEXCOORD0;
					varying highp vec3 vs_NORMAL0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = dot(hlslcc_mtx4x4unity_ObjectToWorld[0].xyz, hlslcc_mtx4x4unity_ObjectToWorld[0].xyz);
					    u_xlat0.w = dot(hlslcc_mtx4x4unity_ObjectToWorld[1].xyz, hlslcc_mtx4x4unity_ObjectToWorld[1].xyz);
					    u_xlat0.xy = sqrt(u_xlat0.xw);
					    u_xlat6 = dot(hlslcc_mtx4x4unity_ObjectToWorld[2].xyz, hlslcc_mtx4x4unity_ObjectToWorld[2].xyz);
					    u_xlat0.z = sqrt(u_xlat6);
					    u_xlat0.xyz = in_NORMAL0.xyz / u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    vs_NORMAL0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 100
					
					#ifdef GL_FRAGMENT_PRECISION_HIGH
					    precision highp float;
					#else
					    precision mediump float;
					#endif
					precision highp int;
					uniform 	vec4 _ColorOverlay;
					uniform 	vec4 _NormalScale;
					uniform 	float _Multiply;
					uniform lowp sampler2D _MainTex;
					uniform lowp sampler2D _LightGradient;
					varying highp vec2 vs_TEXCOORD0;
					varying highp vec3 vs_NORMAL0;
					#define SV_Target0 gl_FragData[0]
					vec4 u_xlat0;
					mediump vec4 u_xlat16_0;
					lowp vec4 u_xlat10_0;
					vec4 u_xlat1;
					lowp vec4 u_xlat10_1;
					void main()
					{
					    u_xlat0.xyz = vs_NORMAL0.xyz * _NormalScale.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
					    u_xlat0.x = u_xlat0.x * 0.333333343;
					    u_xlat10_0 = texture2D(_LightGradient, u_xlat0.xx);
					    u_xlat10_1 = texture2D(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat10_1 * _ColorOverlay;
					    u_xlat16_0 = u_xlat10_0 * u_xlat1;
					    u_xlat0 = u_xlat16_0 * vec4(_Multiply);
					    SV_Target0 = u_xlat0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier00 " {
					"!!GLES3
					#ifdef VERTEX
					#version 300 es
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	vec4 _MainTex_ST;
					in highp vec4 in_POSITION0;
					in highp vec2 in_TEXCOORD0;
					in highp vec3 in_NORMAL0;
					out highp vec2 vs_TEXCOORD0;
					out highp vec3 vs_NORMAL0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = dot(hlslcc_mtx4x4unity_ObjectToWorld[0].xyz, hlslcc_mtx4x4unity_ObjectToWorld[0].xyz);
					    u_xlat0.w = dot(hlslcc_mtx4x4unity_ObjectToWorld[1].xyz, hlslcc_mtx4x4unity_ObjectToWorld[1].xyz);
					    u_xlat0.xy = sqrt(u_xlat0.xw);
					    u_xlat6 = dot(hlslcc_mtx4x4unity_ObjectToWorld[2].xyz, hlslcc_mtx4x4unity_ObjectToWorld[2].xyz);
					    u_xlat0.z = sqrt(u_xlat6);
					    u_xlat0.xyz = in_NORMAL0.xyz / u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    vs_NORMAL0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					uniform 	vec4 _ColorOverlay;
					uniform 	vec4 _NormalScale;
					uniform 	float _Multiply;
					uniform mediump sampler2D _MainTex;
					uniform mediump sampler2D _LightGradient;
					in highp vec2 vs_TEXCOORD0;
					in highp vec3 vs_NORMAL0;
					layout(location = 0) out mediump vec4 SV_Target0;
					vec4 u_xlat0;
					mediump vec4 u_xlat16_0;
					vec4 u_xlat1;
					mediump vec4 u_xlat16_1;
					void main()
					{
					    u_xlat0.xyz = vs_NORMAL0.xyz * _NormalScale.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
					    u_xlat0.x = u_xlat0.x * 0.333333343;
					    u_xlat16_0 = texture(_LightGradient, u_xlat0.xx);
					    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat16_1 * _ColorOverlay;
					    u_xlat16_0 = u_xlat16_0 * u_xlat1;
					    u_xlat0 = u_xlat16_0 * vec4(_Multiply);
					    SV_Target0 = u_xlat0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier01 " {
					"!!GLES3
					#ifdef VERTEX
					#version 300 es
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	vec4 _MainTex_ST;
					in highp vec4 in_POSITION0;
					in highp vec2 in_TEXCOORD0;
					in highp vec3 in_NORMAL0;
					out highp vec2 vs_TEXCOORD0;
					out highp vec3 vs_NORMAL0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = dot(hlslcc_mtx4x4unity_ObjectToWorld[0].xyz, hlslcc_mtx4x4unity_ObjectToWorld[0].xyz);
					    u_xlat0.w = dot(hlslcc_mtx4x4unity_ObjectToWorld[1].xyz, hlslcc_mtx4x4unity_ObjectToWorld[1].xyz);
					    u_xlat0.xy = sqrt(u_xlat0.xw);
					    u_xlat6 = dot(hlslcc_mtx4x4unity_ObjectToWorld[2].xyz, hlslcc_mtx4x4unity_ObjectToWorld[2].xyz);
					    u_xlat0.z = sqrt(u_xlat6);
					    u_xlat0.xyz = in_NORMAL0.xyz / u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    vs_NORMAL0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					uniform 	vec4 _ColorOverlay;
					uniform 	vec4 _NormalScale;
					uniform 	float _Multiply;
					uniform mediump sampler2D _MainTex;
					uniform mediump sampler2D _LightGradient;
					in highp vec2 vs_TEXCOORD0;
					in highp vec3 vs_NORMAL0;
					layout(location = 0) out mediump vec4 SV_Target0;
					vec4 u_xlat0;
					mediump vec4 u_xlat16_0;
					vec4 u_xlat1;
					mediump vec4 u_xlat16_1;
					void main()
					{
					    u_xlat0.xyz = vs_NORMAL0.xyz * _NormalScale.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
					    u_xlat0.x = u_xlat0.x * 0.333333343;
					    u_xlat16_0 = texture(_LightGradient, u_xlat0.xx);
					    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat16_1 * _ColorOverlay;
					    u_xlat16_0 = u_xlat16_0 * u_xlat1;
					    u_xlat0 = u_xlat16_0 * vec4(_Multiply);
					    SV_Target0 = u_xlat0;
					    return;
					}
					
					#endif"
				}
				SubProgram "gles3 hw_tier02 " {
					"!!GLES3
					#ifdef VERTEX
					#version 300 es
					
					uniform 	vec4 hlslcc_mtx4x4unity_ObjectToWorld[4];
					uniform 	vec4 hlslcc_mtx4x4unity_MatrixVP[4];
					uniform 	vec4 _MainTex_ST;
					in highp vec4 in_POSITION0;
					in highp vec2 in_TEXCOORD0;
					in highp vec3 in_NORMAL0;
					out highp vec2 vs_TEXCOORD0;
					out highp vec3 vs_NORMAL0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat6;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = in_POSITION0.yyyy * hlslcc_mtx4x4unity_ObjectToWorld[1];
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = hlslcc_mtx4x4unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + hlslcc_mtx4x4unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * hlslcc_mtx4x4unity_MatrixVP[1];
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = hlslcc_mtx4x4unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = hlslcc_mtx4x4unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    u_xlat0.x = dot(hlslcc_mtx4x4unity_ObjectToWorld[0].xyz, hlslcc_mtx4x4unity_ObjectToWorld[0].xyz);
					    u_xlat0.w = dot(hlslcc_mtx4x4unity_ObjectToWorld[1].xyz, hlslcc_mtx4x4unity_ObjectToWorld[1].xyz);
					    u_xlat0.xy = sqrt(u_xlat0.xw);
					    u_xlat6 = dot(hlslcc_mtx4x4unity_ObjectToWorld[2].xyz, hlslcc_mtx4x4unity_ObjectToWorld[2].xyz);
					    u_xlat0.z = sqrt(u_xlat6);
					    u_xlat0.xyz = in_NORMAL0.xyz / u_xlat0.xyz;
					    u_xlat1.xyz = u_xlat0.yyy * hlslcc_mtx4x4unity_ObjectToWorld[1].xyz;
					    u_xlat0.xyw = hlslcc_mtx4x4unity_ObjectToWorld[0].xyz * u_xlat0.xxx + u_xlat1.xyz;
					    vs_NORMAL0.xyz = hlslcc_mtx4x4unity_ObjectToWorld[2].xyz * u_xlat0.zzz + u_xlat0.xyw;
					    return;
					}
					
					#endif
					#ifdef FRAGMENT
					#version 300 es
					
					precision highp float;
					precision highp int;
					uniform 	vec4 _ColorOverlay;
					uniform 	vec4 _NormalScale;
					uniform 	float _Multiply;
					uniform mediump sampler2D _MainTex;
					uniform mediump sampler2D _LightGradient;
					in highp vec2 vs_TEXCOORD0;
					in highp vec3 vs_NORMAL0;
					layout(location = 0) out mediump vec4 SV_Target0;
					vec4 u_xlat0;
					mediump vec4 u_xlat16_0;
					vec4 u_xlat1;
					mediump vec4 u_xlat16_1;
					void main()
					{
					    u_xlat0.xyz = vs_NORMAL0.xyz * _NormalScale.xyz;
					    u_xlat0.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
					    u_xlat0.x = dot(u_xlat0.xyz, vec3(1.0, 1.0, 1.0));
					    u_xlat0.x = u_xlat0.x * 0.333333343;
					    u_xlat16_0 = texture(_LightGradient, u_xlat0.xx);
					    u_xlat16_1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1 = u_xlat16_1 * _ColorOverlay;
					    u_xlat16_0 = u_xlat16_0 * u_xlat1;
					    u_xlat0 = u_xlat16_0 * vec4(_Multiply);
					    SV_Target0 = u_xlat0;
					    return;
					}
					
					#endif"
				}
			}
			Program "fp" {
				SubProgram "gles hw_tier00 " {
					"!!GLES"
				}
				SubProgram "gles hw_tier01 " {
					"!!GLES"
				}
				SubProgram "gles hw_tier02 " {
					"!!GLES"
				}
				SubProgram "gles3 hw_tier00 " {
					"!!GLES3"
				}
				SubProgram "gles3 hw_tier01 " {
					"!!GLES3"
				}
				SubProgram "gles3 hw_tier02 " {
					"!!GLES3"
				}
			}
		}
	}
}