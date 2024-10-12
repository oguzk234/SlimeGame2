using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance;
    //[SerializeField] private Vector2 FightCamLoc;

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
    public float F1DEnemyAttackHitPercent;
    public Vector2 AttackAnimUp;
    public Vector2 AttackAnimDown;
    public Vector2 AttackAnimRight;
    public Vector2 AttackAnimLeft;
    public AudioSource audioSourceTakeDamage;
    public GameObject DamageTextPrefab;
    public AnimationCurve DamageTextCurve;
    public AnimationCurve DamageTextFadeOutCurve;




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
        CameraFollow.CamObj.transform.position = new Vector3(FightScene.transform.position.x, FightScene.transform.position.y, CameraFollow.CamObj.transform.position.z);

        Fight1DodgeManager fight1DodgeManager = Instantiate(Fight1DodgeManagerPrefab,FightScene.transform).GetComponent<Fight1DodgeManager>();
        fight1DodgeManager.Initialize(fight);
    }

    public void FinishFight1Dodge(Fight1DodgeManager fightManager)
    {
        CameraFollow.FollowingPlayer = true;
        PlayerStats.Instance.SetOpenWorldAction(true);
        Destroy(fightManager.gameObject);
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
    public int enemyAttackMaxComboBase;
    public int enemyAttackMaxComboRange;
    public int enemyAttackCombo
    {
        get
        {
            return OguzLib.Others.GetRandomIntWithOffset(enemyAttackMaxComboBase, enemyAttackMaxComboRange);
        }
    }
    public float enemyAttackCDBase;
    public float enemyAttackCDRange;
    public float enemyAttackCD
    {
        get { return OguzLib.Others.GetRandomFloatWithOffset(enemyAttackCDBase, enemyAttackCDRange); }
    }

    public float enemyAttackTime = 0.2f;

    public int HP;
    public int DamageRandomExtra;
    public int DamageBase;
    public int Damage
    {
        get { return Random.Range(DamageBase - DamageRandomExtra, DamageBase + DamageRandomExtra); }
    }

    public int MaxAttackCount = 3;

    public float PlayerAttackTime = 3f;


    [Header("SpaceSpam Settings")]
    public float ActiveSpaceSpamPowa;
    public float ActiveSpaceSpamPowaPercentage;
    public float SpaceSpamBasePowa = 0.1f;
    public float SpaceSpamIncrementalPowa = 0.3f;
    public float SpaceSpamMinVal = -4.5f;  //ZORLUGU ETKILIYOR ELLEME COK
    public float SpaceSpamMaxVal = 5;
    public float SpaceSpamDecrease = 9;
    public float SpaceSpamYOffset = 0;


    public Fight1Dodge()
    {

    }

    public Vector2 PlayerAttackPos;


}


[System.Serializable]
public class Enemy
{
    public Sprite sprite;




}
