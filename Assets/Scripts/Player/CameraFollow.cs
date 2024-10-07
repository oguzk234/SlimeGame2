using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static GameObject CamObj;
    public static bool FollowingPlayer = true;

    public static MapLimits ActiveMapLimits;
    public MapLimits LastMapLimits;


    private void Awake()
    {
        CamObj = Camera.main.gameObject;
        ActiveMapLimits = LastMapLimits;
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


