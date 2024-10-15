using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSlider : MonoBehaviour
{
    public Vector2 MovePoint1;
    public Vector2 MovePoint2;

    public Vector2 PlayerPoint1;
    public Vector2 PlayerPoint2;

    //public float MoveRate;

    private void Update()
    {
        float moveX = OguzLib.Others.LinearValueConvert(CameraFollow.CamObj.transform.position.x, PlayerPoint1.x, PlayerPoint2.x, MovePoint1.x, MovePoint2.x);               //print(moveX);
        float moveY = OguzLib.Others.LinearValueConvert(CameraFollow.CamObj.transform.position.y, PlayerPoint1.y, PlayerPoint2.y, MovePoint1.y, MovePoint2.y);
        Vector3 Pos = new Vector3(moveX, moveY, transform.position.z);

        this.gameObject.transform.position = Pos;
        BlockData.MakePixelPerfectStatic(transform);    ////////KARAKTERIN FADEOUT FADEIN OLMA SURESINI HASRAR ALMA COOLDOWNLUYLA ESITLEMEYI UNUTMA   ////ZATEN OYLEYMIS XD   /////ILK AUTOFIGHT IN USTUNDE DUVARA GIRIYO OF
    }
}
