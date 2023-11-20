Shader "Unlit/ZaubrrShader"
{
    Properties //свойства, которые показываются в эдиторе
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull off 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert  //pragma - инструкции, которые юнити передает сам себе
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"  //импортирование библиотеки с функциями

            struct appdata //функцию нельзя использовать, пока ее не объявить. Эта строка объявление структуры
            {
                float4 vertex : POSITION; //после двоеточия определяется, что за переменная
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f //v2f - vertex to fragment переход от вершинного шейдера к фрагментному. Пишем то, что хотим передать
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.normal = v.normal; //пишем что является нормалью. o.normal не проходит через интерполятор
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                float3 norm = mul(UNITY_MATRIX_M, float4(i.normal, 0));
                UNITY_APPLY_FOG(i.fogCoord, col);
                return float4(norm, 1);  //делаем нормали глобальными //abs(i.normal); //возвращает визуальную часть. Без abs черное это то где ось уходит в минус, а цвета с отрицательным значением не существуют поэтому рисуется черный. Нормали считаются локально!!
            }
            ENDCG
        }
    }
}
