using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlockData))]
[RequireComponent(typeof(BoxCollider2D))]
public class Teleporter : MonoBehaviour
{
    public bool isActive;
    public Teleporter ToTp;

    public void Teleport()
    {
        PlayerMove.Instance.gameObject.transform.position = ToTp.transform.position;
    }
}
