Shader "custom/Wrap"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Speed("Rotate Speed",Range(0,4)) = 1
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag			
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				float _Speed;

				struct a2v {
					float4 vertex:POSITION;
					float4 texcoord:TEXCOORD;
				};
				struct v2f {
					float4 pos:POSITION;
					float4 uv:texcoord;
				};

				v2f vert(a2v v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord;
					return o;
				}

				fixed4 frag(v2f i) :SV_Target{
					//扭曲效果
					fixed2 uv = i.uv - fixed2(0.5,0.5);
					float angle = _Speed * 0.1745 / (length(uv) + 0.1);//加0.1是放置length（uv）为0
					float angle2 = angle * _Time.y;
					uv = float2(uv.x*cos(angle2) - uv.y*sin(angle2),uv.y*cos(angle2) + uv.x*sin(angle2));
					uv += fixed2(0.5,0.5);
					fixed4 c = tex2D(_MainTex,uv);
					return c;
				}

				ENDCG
			}
		}
}