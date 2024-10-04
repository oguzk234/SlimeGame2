using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject CamObj;
    public bool FollowingPlayer;

    public static MapLimits ActiveMapLimits;
    public MapLimits StartMapLimits;


    private void Awake()
    {
        CamObj = Camera.main.gameObject;
        ActiveMapLimits = StartMapLimits;
    }
    void LateUpdate()
    {
        if (FollowingPlayer)
        {
            CamObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, CamObj.transform.position.z);
            CamObj.transform.position = new Vector3(Mathf.Clamp(CamObj.transform.position.x, ActiveMapLimits.Limit1.x + 12f, ActiveMapLimits.Limit2.x - 12f), Mathf.Clamp(CamObj.transform.position.y, ActiveMapLimits.Limit1.y + 6.75f, ActiveMapLimits.Limit2.y - 6.75f),CamObj.transform.position.z);
        }

        CameraDebug();
    }


    private void CameraDebug()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            FollowingPlayer = !FollowingPlayer;
        }
    }
}

[System.Serializable]
public struct MapLimits
{
    public Vector3 Limit1;
    public Vector3 Limit2;
    public MapLimits(Vector2 LeftDownCorner, Vector2 RightTopCorner)
    {
        Limit1 = LeftDownCorner;
        Limit2 = RightTopCorner;
    }
}
