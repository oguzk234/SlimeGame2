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
    }
}
