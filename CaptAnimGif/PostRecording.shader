Shader "Custom/PostRecording" {
	Properties{
		_MainTex("Texture", 2D) = "white"{}
	}
	SubShader {
		Pass {
			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert_img
			#pragma fragment frag

			sampler2D _MainTex;

			fixed4 frag(v2f_img i) : COLOR {
				fixed4 c = tex2D(_MainTex, i.uv);
					if ((i.uv.x < 0.01f || i.uv.x > 0.99f)) {
						return fixed4(1.0f, 0.1f, 0.1f, 1);
					}
				return c;
			}

			ENDCG
		}
	}
}
