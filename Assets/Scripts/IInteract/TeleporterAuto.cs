using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterAuto : MonoBehaviour, IInteractableAuto
{
    public bool isActive;
    public Area area;

    public void InteractAuto()
    {
        if (isActive)
        {
            SceneManager.Instance.ProceedArea(area);
            area.InitializeSpriteOverride();
        }
    }
}
