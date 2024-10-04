using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public List<ItemBase> Inventory = new List<ItemBase>();

    private void Awake()
    {
        Instance = this;
    }
}
