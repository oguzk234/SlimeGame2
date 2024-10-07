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
    public static void MakePixelPerfectStatic(Transform transform)
    {
        // Her ekseni ayrý ayrý en yakýn grid boyutuna yuvarla
        float x = Mathf.Round(transform.position.x / 0.0625f) * 0.0625f;
        float y = Mathf.Round(transform.position.y / 0.0625f) * 0.0625f;

        transform.position = new Vector2(x, y);
    }



    public MapLimits GetLimits()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Bounds bounds = spriteRenderer.sprite.bounds;


        Vector3 bottomLeft = new Vector3(bounds.min.x, bounds.min.y, 0);
        Vector3 topRight = new Vector3(bounds.max.x, bounds.max.y, 0);

        Vector2 worldBottomLeft = spriteRenderer.transform.TransformPoint(bottomLeft);
        Vector2 worldTopRight = spriteRenderer.transform.TransformPoint(topRight);

        return new MapLimits(worldBottomLeft, worldTopRight);
    }
}


