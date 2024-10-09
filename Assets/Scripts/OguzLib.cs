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
        }

        public static IEnumerator WaitAndExecute(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        public static List<T> GetRandomUniqueElements<T>(List<T> list, int numberOfElements)
        {
            // E�er istenen eleman say�s� listedeki eleman say�s�ndan b�y�kse, listeyi d�ner
            if (numberOfElements > list.Count)
            {
                Debug.LogWarning("�stenilen eleman say�s�, listedeki eleman say�s�ndan fazla!");
                return new List<T>(list);
            }

            // Yeni bir liste olu�tur, rastgele elemanlar� saklamak i�in
            List<T> randomElements = new List<T>();

            // Orijinal listeyi bir kopya olarak al ve �zerinde de�i�iklik yap
            List<T> tempList = new List<T>(list);

            for (int i = 0; i < numberOfElements; i++)
            {
                // Rastgele bir eleman se�
                int randomIndex = UnityEngine.Random.Range(0, tempList.Count);
                T selectedElement = tempList[randomIndex];

                // Se�ilen eleman� rastgele listeye ekle
                randomElements.Add(selectedElement);

                // Se�ilen eleman� ge�ici listeden ��kar ki tekrar se�ilmesin
                tempList.RemoveAt(randomIndex);
            }

            return randomElements;
        }

        public static float GetRandomNumber(float ValueBase,float ValueMaxRandomOffset)
        {
            return UnityEngine.Random.Range(ValueBase - ValueMaxRandomOffset, ValueBase + ValueMaxRandomOffset);
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

            // Renk bile�enlerini RRGGBB format�na d�n��t�r
            string hexColor = string.Format("{0:X2}{1:X2}{2:X2}", r, g, b);

            return hexColor;
        }
        public static Color HexCodeToColor(string hexColorCode)
        {
            // E�er renk kodu 6 veya 8 karaktere sahip de�ilse, uygun formata getir
            if (hexColorCode.Length != 6 && hexColorCode.Length != 8)
            {
                Debug.LogError("Ge�ersiz renk kodu: " + hexColorCode);
                return Color.white; // Varsay�lan olarak beyaz renk
            }

            // E�er 8 karakterli bir renk kodu ise, alpha bile�enini al
            float alpha = 1f;
            if (hexColorCode.Length == 8)
            {
                string alphaHex = hexColorCode.Substring(6, 2);
                alpha = (float)System.Convert.ToInt32(alphaHex, 16) / 255f;
            }

            // R, G, B bile�enlerini al
            string rHex = hexColorCode.Substring(0, 2);
            string gHex = hexColorCode.Substring(2, 2);
            string bHex = hexColorCode.Substring(4, 2);

            // Renk bile�enlerini d�n��t�r
            float r = (float)System.Convert.ToInt32(rHex, 16) / 255f;
            float g = (float)System.Convert.ToInt32(gHex, 16) / 255f;
            float b = (float)System.Convert.ToInt32(bHex, 16) / 255f;

            // Color nesnesini olu�tur ve d�nd�r
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


    }

}

