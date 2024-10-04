using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemBase
{
    public string itemName;
    public string itemInfo;


    public virtual void UseItem()
    {

    }

    public virtual void ShowInfo()
    {

    }

    public void DestroyItem()
    {
        if (PlayerInventory.Instance.Inventory.Contains(this))
        {
            PlayerInventory.Instance.Inventory.Remove(this);

        }
        else
        {
            Debug.LogError("OLMAYAN ITEMI NASIL KULLANIYON !!??!!??!!");
        }
    }
}
