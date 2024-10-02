using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        print("ELMA ALINDI");
        //Destroy(this.gameObject);
    }


}
