using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockData : MonoBehaviour
{
    public bool isBlocking;

    public void MakePixelPerfect()
    {
        // Her ekseni ayrý ayrý en yakýn grid boyutuna yuvarla
        float x = Mathf.Round(transform.position.x / 0.0625f) * 0.0625f;
        float y = Mathf.Round(transform.position.y / 0.0625f) * 0.0625f;

        transform.position = new Vector2(x, y);
    }
}


