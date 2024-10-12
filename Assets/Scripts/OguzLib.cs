using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace OguzLib
{
    public static class Others
    {
        public static float LinearValueConvert(float value, float oldValueMin, float oldValueMax, float newValueMin, float newValueMax)
        {
            return ((newValueMax - newValueMin) / (oldValueMax - oldValueMin)) * (value - oldValueMin) + newValueMin;
        }   //BUNUN YERINE    MATHF.LERP()    KULLANSANA AMK

        public static IEnumerator WaitAndExecute(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        public static List<T> GetRandomUniqueElements<T>(List<T> list, int numberOfElements)
        {
            // Eðer istenen eleman sayýsý listedeki eleman sayýsýndan büyükse, listeyi döner
            if (numberOfElements > list.Count)
            {
                Debug.LogWarning("Ýstenilen eleman sayýsý, listedeki eleman sayýsýndan fazla!");
                return new List<T>(list);
            }

            // Yeni bir liste oluþtur, rastgele elemanlarý saklamak için
            List<T> randomElements = new List<T>();

            // Orijinal listeyi bir kopya olarak al ve üzerinde deðiþiklik yap
            List<T> tempList = new List<T>(list);

            for (int i = 0; i < numberOfElements; i++)
            {
                // Rastgele bir eleman seç
                int randomIndex = UnityEngine.Random.Range(0, tempList.Count);
                T selectedElement = tempList[randomIndex];

                // Seçilen elemaný rastgele listeye ekle
                randomElements.Add(selectedElement);

                // Seçilen elemaný geçici listeden çýkar ki tekrar seçilmesin
                tempList.RemoveAt(randomIndex);
            }

            return randomElements;
        }

        public static float GetRandomFloatWithOffset(float ValueBase,float ValueMaxRandomOffset) //Balancer sagdaki sayiyinin çýkma ihtimalini düzeltmek için  //BALANCERA GEREK YOK SILINDI
        {
            return UnityEngine.Random.Range(ValueBase - ValueMaxRandomOffset, ValueBase + ValueMaxRandomOffset);
        }

        public static int GetRandomIntWithOffset(int ValueBase, int ValueMaxRandomOffset)
        {
            return UnityEngine.Random.Range(ValueBase - ValueMaxRandomOffset, ValueBase + ValueMaxRandomOffset + 1);
        }

        public static float CalculatePercentage(float lowerLimit, float upperLimit, float value)
        {
            // Aralýk hesapla
            float range = upperLimit - lowerLimit;

            // Verilen sayýnýn aralýk içindeki uzaklýðýný bul
            float distance = value - lowerLimit;

            // Yüzdeyi hesapla
            float percentage = (distance / range) * 100;

            return percentage;
        }

        public static float InverseGrow(float x)
        {
            // x sýfýra yaklaþtýkça sonuç artmalý.
            // Küçük x deðerleri için daha büyük sonuç döndürmek için ters orantý kullanýyoruz.

            if (x == 0) // x sýfýrsa sonsuz olur, bu durumda özel bir durum belirtebiliriz.
            {
                Debug.LogError("x sýfýr olamaz!");
                return float.MaxValue; // veya baþka bir özel deðer dönebilirsiniz.
            }

            return 1 / x; // x küçüldükçe sonuç büyüyecek
        }


    }

    public static class SubObject
    {
        #region SubOjbectThings

        public static Transform FindTopTransform(Transform transform)
        {
            Transform currentTransform = transform;

            while (currentTransform.parent != null)
            {
                currentTransform = currentTransform.parent;
            }
            return currentTransform;
        }

        public static List<Transform> GetAllSubTransforms(Transform parent)
        {
            List<Transform> transforms = new List<Transform>();

            int childCount = parent.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                Transform child = parent.GetChild(i);
                transforms.Add(child);
            }

            return transforms;
        }

        public static List<GameObject> GetAllSubGameObjects(GameObject parent)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            int childCount = parent.transform.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                GameObject child = parent.transform.GetChild(i).gameObject;
                gameObjects.Add(child);
            }

            return gameObjects;
        }
        #endregion
    }

    public static class Colors
    {
        #region ColorTransforms
        public static string ColorToHexCode(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255f);
            int g = Mathf.RoundToInt(color.g * 255f);
            int b = Mathf.RoundToInt(color.b * 255f);

            // Renk bileþenlerini RRGGBB formatýna dönüþtür
            string hexColor = string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);

            return hexColor;
        }
        public static Color HexCodeToColor(string hexColorCode)
        {
            // Eðer renk kodu 6 veya 8 karaktere sahip deðilse, uygun formata getir
            if (hexColorCode.Length != 6 && hexColorCode.Length != 8)
            {
                Debug.LogError("Geçersiz renk kodu: " + hexColorCode);
                return Color.white; // Varsayýlan olarak beyaz renk
            }

            // Eðer 8 karakterli bir renk kodu ise, alpha bileþenini al
            float alpha = 1f;
            if (hexColorCode.Length == 8)
            {
                string alphaHex = hexColorCode.Substring(6, 2);
                alpha = (float)System.Convert.ToInt32(alphaHex, 16) / 255f;
            }

            // R, G, B bileþenlerini al
            string rHex = hexColorCode.Substring(0, 2);
            string gHex = hexColorCode.Substring(2, 2);
            string bHex = hexColorCode.Substring(4, 2);

            // Renk bileþenlerini dönüþtür
            float r = (float)System.Convert.ToInt32(rHex, 16) / 255f;
            float g = (float)System.Convert.ToInt32(gHex, 16) / 255f;
            float b = (float)System.Convert.ToInt32(bHex, 16) / 255f;

            // Color nesnesini oluþtur ve döndür
            return new Color(r, g, b, alpha);
        }
        #endregion
    }

    public static class Vectors
    {
        public static Vector2[] VectorDirections4 = new Vector2[4]
{
            new Vector2(0, 1),
            new Vector2(0, -1),
            new Vector2(1, 0),
            new Vector2(-1, 0)
};
        public static Vector2[] VectorDirections8 = new Vector2[8]
        {
            new Vector2(0, 1),
            new Vector2(0, -1),
            new Vector2(1, 0),
            new Vector2(-1, 0),

            new Vector2(1, 1),
            new Vector2(1, -1),
            new Vector2(-1, 1),
            new Vector2(-1, -1)
        };


        public static Vector2 ReturnRandomDirection4()
        {
            return VectorDirections4[UnityEngine.Random.Range(0, VectorDirections4.Length)];  //Length e "-1" EKLEME SONRA GELMIYO
        }
        public static Vector2 ReturnRandomDirection8()
        {
            return VectorDirections8[UnityEngine.Random.Range(0, VectorDirections8.Length)];
        }
        
        public static Vector2[] ReturnRandomDirections4(int numberOfVectors)
        {
            Vector2[] vectors = new Vector2[numberOfVectors];
            List<Vector2> vectorsAll4 = new List<Vector2>();

            foreach(Vector2 vec2 in VectorDirections4) { vectorsAll4.Add(vec2); }

            for (int i = 0; i < numberOfVectors; i++)
            {
                Vector2 vectorToAdd = vectorsAll4[UnityEngine.Random.Range(0, vectorsAll4.Count)];
                vectorsAll4.Remove(vectorToAdd);
                vectors[i] = vectorToAdd;
            }

            return vectors;

        }


    }

}

