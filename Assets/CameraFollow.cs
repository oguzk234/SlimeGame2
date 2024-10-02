using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject CamObj;
    public bool FollowingPlayer;


    private void Awake()
    {
        CamObj = Camera.main.gameObject;
    }
    void LateUpdate()
    {
        if (FollowingPlayer)
        {
            CamObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, CamObj.transform.position.z);
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
