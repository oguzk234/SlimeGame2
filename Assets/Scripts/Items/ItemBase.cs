using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ItemBase
{
    public string itemName;
    public string itemInfo;
    //public Image itemImage;
    public Sprite itemSprite;


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
            PlayerInventory.Instance.UpdateInventoryUI();

        }
        else
        {
            Debug.LogError("OLMAYAN ITEMI NASIL KULLANIYON !!??!!??!!");
        }
    }
}
