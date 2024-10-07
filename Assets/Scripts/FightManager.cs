using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance;
    [SerializeField] private Vector2 FightCamLoc;


    public Fight1Dodge fg1;

    public GameObject FightScene;
    public GameObject Fight1DodgeManagerPrefab;

    /*
    [Header("Fight1ReferencesForManager")]
    public GameObject MCFight1Dodge;
    */


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartFight1Dodge(fg1);
        }
    }
    public void StartFight1Dodge(Fight1Dodge fight)
    {
        CameraFollow.FollowingPlayer = false;
        CameraFollow.CamObj.transform.position = new Vector3(FightCamLoc.x, FightCamLoc.y, CameraFollow.CamObj.transform.position.z);

        Fight1DodgeManager fight1DodgeManager = Instantiate(Fight1DodgeManagerPrefab,FightScene.transform).GetComponent<Fight1DodgeManager>();
        fight1DodgeManager.Initialize(fight);
    }


}


[System.Serializable]
public class Fight
{
    public Enemy enemy;

}

[System.Serializable]
public class Fight1Dodge : Fight
{
    public Fight1Dodge()
    {

    }




}


[System.Serializable]
public class Enemy
{
    public Sprite sprite;

    public int HP;
    public int DamageRandomExtra;
    public int DamageBase;
    public int Damage
    {
        get { return Random.Range(DamageBase - DamageRandomExtra, DamageBase + DamageRandomExtra); }
    }




}
