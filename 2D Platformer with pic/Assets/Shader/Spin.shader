Shader "custom/Spin" {
	Properties{
			_Color("Color", Color) = (1,1,1,1)
			_MainTex("Albedo (RGB)", 2D) = "white" {}
			_RSpeed("RotateSpeed", Range(1, 100)) = 2	//旋转速度

	}


		SubShader{
				//Tags { "RenderType"="Opaque" }
				//贴图带透明通道，半透明效果设置：
				Tags{"Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True"}
				//Blend选值为：SrcAlpha  和OneMinusSrcAlpha（即1-SrcAlpha）
				Blend SrcAlpha OneMinusSrcAlpha
				Pass{
					Name "Simple"
					Cull off //双面都显示

					CGPROGRAM
					#pragma vertex vert
					#pragma fragment frag
					#include "UnityCG.cginc"

					sampler2D _MainTex; //变量使用前声明
					float4 _Color;
					float _RSpeed;

					struct v2f {
						float4 pos:POSITION;
						float4 uv:TEXCOORD0;
					};

					v2f vert(appdata_base v) {
						v2f o;
						o.pos = UnityObjectToClipPos(v.vertex);
						o.uv = v.texcoord;
						return o;
					}

					half4 frag(v2f i) :COLOR{

						float2 uv = i.uv.xy - float2(0.5, 0.5);//UV原点移动到UV中心点

						//float2 rotate = float2(cos(_RSpeed *_Time.x), sin(_RSpeed *_Time.x));
						//uv = float2(uv.x * rotate.x - uv.y * rotate.y, uv.x * rotate.y + uv.y * rotate.x)
						//θ旋转角度 UV旋转 (xcosθ - ysinθ,xsinθ+ycosθ)
						uv = float2(uv.x *cos(-_RSpeed * _Time.x) - uv.y * sin(-_RSpeed * _Time.x),uv.x *sin(-_RSpeed * _Time.x) + uv.y*cos(-_RSpeed * _Time.x));
						uv += float2(0.5, 0.5);//UV中心转移回原来原点位置
						half4 c = tex2D(_MainTex, uv) * _Color;
						return c;
					}

					ENDCG
				}
			}
				FallBack "Diffuse"
}