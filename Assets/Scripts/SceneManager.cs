using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;
    public Area ActiveArea;

    void Awake()
    {
        Instance = this;
    }

    public void ProceedArea(Area area)
    {
        OnAreaChanged(ActiveArea, area);
        ActiveArea = area;

        CameraFollow.ActiveMapLimits = area.mapLimits;
        PlayerMove.Instance.gameObject.transform.position = area.startLocation;
    }


    public void OnAreaChanged(Area oldArea, Area newArea)
    {
        oldArea.SetAreaComponentsActive(false);
        newArea.SetAreaComponentsActive(true);
    }


}




[System.Serializable]
public class Area
{
    public MapLimits mapLimits;
    public Vector2 startLocation;
    public SpriteRenderer Renderer;
    public Sprite NewSprite;
    public List<Behaviour> AreaComponents;

    public void InitializeSpriteOverride()
    {
        if(Renderer != null && NewSprite != null) { Renderer.sprite = NewSprite; }
    }

    public void SetAreaComponentsActive(bool bol)
    {
        foreach(Behaviour behaviour in AreaComponents)
        {
            behaviour.enabled = bol;
        }
    }


    

    /*   GEREKSIZ???
    public Area(MapLimits MapLimitss, Vector2 StartLocationn, Sprite spriteToOverride = null)
    {
        mapLimits = MapLimitss;
        startLocation = StartLocationn;

        //OPTIONAL
        if(OverridedSprite != null && spriteToOverride != null)
        {
            OverridedSprite.sprite = spriteToOverride;
        }
    }
    */
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
