Shader "Billboard/1Direaction"
{
    Properties //свойства, которые показываютс€ в эдиторе
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "DisableBatching" = "True"
        }
        LOD 100
        Cull off

        Pass
        {
            CGPROGRAM

            #pragma vertex vert  //pragma - инструкции, которые юнити передает сам себе
            #pragma fragment frag // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"  //импортирование библиотеки с функци€ми
            #include "DoomBillboard.cginc"

            struct appdata //функцию нельз€ использовать, пока ее не объ€вить. Ёта строка объ€вление структуры
            {
                float4 vertex : POSITION; //после двоеточи€ определ€етс€, что за переменна€
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f //v2f - vertex to fragment переход от вершинного шейдера к фрагментному. ѕишем то, что хотим передать
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float2 angle : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.normal = v.normal; //пишем что €вл€етс€ нормалью. o.normal не проходит через интерпол€тор

                float3 cameraDir = -1 * mul(UNITY_MATRIX_M, transpose(mul(unity_WorldToObject, UNITY_MATRIX_I_V))[2].xyz);
                cameraDir.y = 0;

                float2 cameraDir2D = normalize(cameraDir.xz);

                float2 vectorForward2D = mul(UNITY_MATRIX_M, float4(0, 0, 1, 0)).xz;

                float angle = dot(vectorForward2D, cameraDir2D);

                float angleRad = acos(angle);

                float3 crossProduct = cross(
                    float3(vectorForward2D.x, 0, vectorForward2D.y),
                    float3(cameraDir.x, 0, cameraDir.y));

                if (dot(crossProduct, float3(0, 1, 0)) < 0)
                    angleRad = -angleRad;

                float angleNormalized = angleRad / 3.1415;

                o.angle = (angleNormalized + 1) / 2;

                float3 newVertex;
                Unity_RotateAboutAxis_Radians_float(v.vertex, float3(0, 1, 0), angleRad, newVertex);

                o.vertex = UnityObjectToClipPos(newVertex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
               fixed4 color = tex2D(_MainTex, i.uv);

                if (color.a < 0.001)
                    discard;

                return color;
            }
            ENDCG
        }
    }
}
