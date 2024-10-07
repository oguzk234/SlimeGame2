using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlockData))]
[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour,IInteractable
{
    public bool isActive;
    public bool neededInteraction = true;

    public Area area;


    public void Interact()
    {
        Teleport();
    }

    public void Teleport()
    {
        if (isActive && neededInteraction)
        {
            SceneManager.Instance.ProceedArea(area);
            area.InitializeSpriteOverride();
        }
    }

    public void TeleportNoInteraction()
    {
        if (isActive)
        {
            SceneManager.Instance.ProceedArea(area);
            area.InitializeSpriteOverride();
        }
    }
}
