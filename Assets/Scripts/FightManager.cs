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

    [Header("GeneralFight1DodgeSettings")]
    public float F1DStartingTime;
    public float F1DDamageTakeCDMax;
    public float F1DTimeBeforeTakeDamageAfterAttackSpawned;  //ESKI
    public float F1DTimePercentBeforeTakeDamageAfterAttackSpawned;
    public float EnemyAttackHitPercent;
    public Vector2 AttackAnimUp;
    public Vector2 AttackAnimDown;
    public Vector2 AttackAnimRight;
    public Vector2 AttackAnimLeft;
    public AudioSource audioSourceTakeDamage;




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
        PlayerStats.Instance.SetOpenWorldAction(false);

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
    public int enemyAttackMaxCombo;
    public int enemyAttackMaxComboRange;
    public float enemyAttackCDBase;
    public float enemyAttackCDRange;
    public float enemyAttackCD
    {
        get { return OguzLib.Others.GetRandomNumber(enemyAttackCDBase, enemyAttackCDRange); }
    }

    public float enemyAttackTime = 0.2f;

    public int HP;
    public int DamageRandomExtra;
    public int DamageBase;
    public int Damage
    {
        get { return Random.Range(DamageBase - DamageRandomExtra, DamageBase + DamageRandomExtra); }
    }


    public Fight1Dodge()
    {

    }




}


[System.Serializable]
public class Enemy
{
    public Sprite sprite;




}
