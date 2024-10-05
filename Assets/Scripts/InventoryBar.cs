using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public class InventoryBar : MonoBehaviour
{
    [SerializeField] private ItemBase Item;
    [SerializeField] private TextMeshProUGUI ItemText;


    public void Initialize(ItemBase itemBase)
    {
        Item = itemBase;
        ItemText.text = Item.itemName;
        

    }
}
