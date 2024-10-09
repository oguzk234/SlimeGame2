using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight1DodgeManager : MonoBehaviour
{
    [Header("REFER")]
    public GameObject MCFight;
    public GameObject EnemyFight;
    public SpriteRenderer EnemyRenderer;
    public Fight1Dodge fight1Dodge;
    public GameObject AttackAnimPrefab;


    [Header("Dodge Settings")]
    public float dodgeTime;
    public float dodgePercent; //ELLEME EDITORDE
    public float neededPercentToDodgeOverride = 0.7f;
    public Vector2 ActiveDodgeDir;
    public AnimationCurve DodgeAnimCurve;
    public float DodgeRange;
    public bool isDodging;
    public Coroutine oldDodgeCoroutine;


    [Header("General Settings")]
    public FightManager fightManager;
    public bool isFightStarted;
    public float DamageTakeCD;
    public Color AttackColor1 = Color.white;
    public Color AttackColor2;

    [Header("EnemySettings")]
    public List<Vector2> ActiveEnemyAttacks = new List<Vector2>();




    private void Start()
    {
        //MCFight = FightManager.Instance.MCFight1Dodge;
        fightManager = FightManager.Instance;
        StartFight();

    }

    public void Initialize(Fight1Dodge f1d)
    {
        fight1Dodge = f1d;

        isFightStarted = true;

        EnemyRenderer.sprite = fight1Dodge.enemy.sprite;
    }

    private void Update()
    {
        if (isFightStarted)
        {
            DamageTakeCD -= Time.deltaTime;

            DodgeInput();

            if ((ActiveEnemyAttacks.Count > 0 && ActiveDodgeDir == Vector2.zero) || (ActiveEnemyAttacks.Contains(ActiveDodgeDir)))   //BURDA NET BI BOK VAR  (COK DA YOKMUS MEKANIK OYLE)
            {
                if (DamageTakeCD < 0)
                {
                    DamageTakeCD = fightManager.F1DDamageTakeCDMax;
                    print("HASAR ALINDI");
                    fightManager.audioSourceTakeDamage.Play();
                }
                else
                {
                    //print("HASAR ALINDI AMA CD BITMEDI");
                }

            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                EnemySingleAttack();
            }

        }

    }



    private void StartFight()
    {
        StartCoroutine(StartFightCoroutine());
    }
    private IEnumerator StartFightCoroutine()
    {
        yield return new WaitForSecondsRealtime(fightManager.F1DStartingTime);
        isFightStarted = true;

        while (isFightStarted)
        {
            EnemySingleAttack();
            yield return new WaitForSecondsRealtime(fight1Dodge.enemyAttackCD);
        }

    }

    private void EnemySingleAttack()
    {
        StartCoroutine(EnemySingleAttackCoroutine());
    }
    private IEnumerator EnemySingleAttackCoroutine()
    {
        Animator attackAnimFX = Instantiate(AttackAnimPrefab, this.transform).GetComponent<Animator>();

        Vector2 attackDir = OguzLib.Vectors.ReturnRandomDirection4();
        Vector2 attackAnimDir = Vector2.zero;
        //attackAnimFX.gameObject.transform.position = new Vector3(attackAnimFX.gameObject.transform.position.x + (attackDir.x * 5),attackAnimFX.gameObject.transform.position.y + (attackDir.y *5),attackAnimFX.gameObject.transform.position.z);

        if(attackDir == Vector2.up)
        {
            attackAnimDir = fightManager.AttackAnimUp;
        }
        else if (attackDir == Vector2.down)
        {
            attackAnimDir = fightManager.AttackAnimDown;
        }
        else if(attackDir == Vector2.right)
        {
            attackAnimDir = fightManager.AttackAnimRight;
        }
        else if (attackDir == Vector2.left)
        {
            attackAnimDir = fightManager.AttackAnimLeft;
        }


        attackAnimFX.gameObject.transform.localPosition = attackAnimDir;

        float elapsedTime = 0f;


        //yield return new WaitForSecondsRealtime(fightManager.F1DTimeBeforeTakeDamageAfterAttackSpawned);  //BASKA COROUTINE E ALINDI ALTTA
        //ActiveEnemyAttacks.Add(attackDir);
        StartCoroutine(EnemySingleAttackAddList(attackDir,attackAnimFX.gameObject.GetComponent<SpriteRenderer>()));



        
        while (elapsedTime < fight1Dodge.enemyAttackTime)
        {
            elapsedTime += Time.deltaTime;
            float animPercent = elapsedTime/fight1Dodge.enemyAttackTime;


            attackAnimFX.Play("AttackFXanim",0,animPercent);
            yield return null;
        }
        




        Destroy(attackAnimFX.gameObject);
        ActiveEnemyAttacks.Remove(attackDir);

    }
    private IEnumerator EnemySingleAttackAddList(Vector2 vector2,SpriteRenderer spriteRenderer)
    {
        //yield return new WaitForSecondsRealtime(fightManager.F1DTimeBeforeTakeDamageAfterAttackSpawned);
        float elapsedTime = 0f;
        float attackPercent = 0f;
        spriteRenderer.color = AttackColor1;

        while(elapsedTime < fight1Dodge.enemyAttackTime)
        {
            elapsedTime += Time.deltaTime;
            attackPercent = elapsedTime / fight1Dodge.enemyAttackTime;

            if (attackPercent > fightManager.F1DTimePercentBeforeTakeDamageAfterAttackSpawned)
            {
                ActiveEnemyAttacks.Add(vector2);
                spriteRenderer.color = AttackColor2;
                yield break;
            }


            yield return null;
        }

        //ActiveEnemyAttacks.Add(vector2);
    }




    private void DodgeInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Dodge(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Dodge(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Dodge(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Dodge(Vector2.up);
        }
    }
    private void Dodge(Vector2 dodgeDir)
    {
        if (isDodging == true) { return; }
        //StopAllCoroutines();
        if(oldDodgeCoroutine != null) { StopCoroutine(oldDodgeCoroutine); }

        oldDodgeCoroutine = StartCoroutine(DodgeCoroutine(dodgeDir));
    }
    private IEnumerator DodgeCoroutine(Vector2 dodgeDir)
    {
        isDodging = true;
        
        float elapsedTime = 0f;

        ActiveDodgeDir = dodgeDir;

        while (elapsedTime < dodgeTime)
        {
            elapsedTime += Time.deltaTime;

            dodgePercent = DodgeAnimCurve.Evaluate(elapsedTime / dodgeTime);   //0dan 1 sonra yine 0 oluyo
            float dodgePercentRaw = elapsedTime / dodgeTime;

            //dodgePercent = (elapsedTime / dodgeTime);
            //float animationPercent = DodgeAnimCurve.Evaluate(dodgeTime);

            MCFight.transform.localPosition = new Vector3(ActiveDodgeDir.x*dodgePercent * DodgeRange, ActiveDodgeDir.y * dodgePercent * DodgeRange, MCFight.transform.position.z);
            BlockData.MakePixelPerfectStatic(MCFight.transform);
            

            if(dodgePercentRaw > neededPercentToDodgeOverride)
            {
                isDodging = false;
            }


            //print(dodgePercent);  //WORKING

            yield return null;
            //yield break; //KOMPLE DURDURUYOR COROUTINE I
        }

        ActiveDodgeDir = Vector2.zero;

        //Debug.Log("Dodgjj tamamlandý!");

        isDodging = false;
    }




}
