using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    public List<Behaviour> OpenWorldActionComponents = new List<Behaviour>();

    public int MaxHealth;
    public int Health;

    public int DamageBase;
    public int DamageRandomOffset;
    public int Damage
    {
        get
        {
            return OguzLib.Others.GetRandomIntWithOffset(DamageBase, DamageRandomOffset);
        }
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)) { PlayerInventory.Instance.Inventory[0].UseItem(); }
    }

    public void SetOpenWorldAction(bool bol) 
    {
        foreach(Behaviour behaviour in OpenWorldActionComponents)
        {
            behaviour.enabled = bol;
        }
    } 
}
