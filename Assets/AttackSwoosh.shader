Shader "Unlit/AttackSwoosh"
{
    Properties
    {
        _progress("_progress",float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Pass
        {
            blend one one
            zwrite off
            
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 circle;
            float Cmask;
            float VisibleRange;
            float SemiMask;
            float ActionMask;
            float finalMask;
            
            float _progress;
            

            v2f vert (appdata v)
            {
                
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                circle = (i.uv.x*2 -1)*(i.uv.x*2 -1) + (i.uv.y*2 -1)*(i.uv.y*2 -1);
                Cmask = circle<=1*i.uv.y && circle>=0.2/i.uv.y;
                SemiMask =Cmask * i.uv.y >=0.5;
                ActionMask = ((i.uv.x*4 -2.69f)*(i.uv.x*4 -2.69f)+(i.uv.y*4-2.5)*(i.uv.y*4-2.5))<2;
                finalMask = SemiMask * ActionMask;
                
                return finalMask* (1- i.uv.x >=_progress);// &&1-i.uv.x <= _progress-0.2) ;
            }
            ENDCG
        }
    }
}
