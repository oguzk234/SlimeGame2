using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockData))]
public class BlockDataEdit : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BlockData blockData = (BlockData)target;

        if (GUILayout.Button("MakePixelPerfect"))
        {
            blockData.MakePixelPerfect();
        }

        if (GUILayout.Button("GetLimits"))
        {
            MapLimits limits = blockData.GetLimits();
            Debug.Log("Limit 1 = "+limits.Limit1.x + " / " + limits.Limit1.y + "  ||| Limit 2 = "+limits.Limit2.x + " / " + limits.Limit2.y);
        }
    }
}
