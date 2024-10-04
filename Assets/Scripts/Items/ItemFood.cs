using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemFood : ItemBase
{
    public int HpRegen;



    public override void UseItem()
    {
        base.UseItem();
        PlayerStats.Instance.Health = Mathf.Clamp(PlayerStats.Instance.Health + this.HpRegen, 0, PlayerStats.Instance.MaxHealth);
        DestroyItem();
    }
}
