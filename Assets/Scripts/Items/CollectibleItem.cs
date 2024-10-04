using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour, IInteractable
{
    public ItemBase itemBase;
    public ItemFood itemFood;

    public void Interact()
    {
        print("ITEM ALINDI");

        if(!string.IsNullOrEmpty(itemBase.itemName)) { PlayerInventory.Instance.Inventory.Add(itemBase); }
        if(!string.IsNullOrEmpty(itemFood.itemName)) { PlayerInventory.Instance.Inventory.Add(itemFood); }



        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }


}
