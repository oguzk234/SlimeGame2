using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInteractAuto : MonoBehaviour, IInteractableAuto
{
    public Fight1Dodge f1d;
    public bool isDone;
    public void InteractAuto()
    {
        if (!isDone)
        {
            isDone = true;
            FightManager.Instance.StartFight1Dodge(f1d);
        }
    }
}
