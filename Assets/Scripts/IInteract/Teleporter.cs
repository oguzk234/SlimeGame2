using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlockData))]
[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour,IInteractable
{
    public bool isActive;
    public Vector2 TpPos;

    public MapLimits NextMapLimits;


    public void Interact()
    {
        Teleport();
    }

    public void Teleport()
    {
        if (isActive)
        {
            PlayerMove.Instance.gameObject.transform.position = TpPos;

            CameraFollow.ActiveMapLimits = NextMapLimits;
        }

    }
}
