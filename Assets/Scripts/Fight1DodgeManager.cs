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
    public GameObject SpaceSpamObj;


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

    [Header("SpaceSpam Settings")]
    public float ActiveSpaceSpamPowa;
    public float ActiveSpaceSpamPowaPercentage;
    public float SpaceSpamBasePowa = 0.1f;
    public float SpaceSpamIncrementalPowa = 0.3f;
    public float SpaceSpamMinVal = -4.5f;  //ZORLUGU ETKILIYOR ELLEME COK
    public float SpaceSpamMaxVal = 5;
    public float SpaceSpamDecrease = 9;
    public float SpaceSpamYOffset = 0;





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
                    PlayerStats.Instance.Health -= fight1Dodge.Damage;


                    DamageTakeCD = fightManager.F1DDamageTakeCDMax;
                    print("HASAR ALINDI  ==  ActýveDodgeDir = " + ActiveDodgeDir + "  Saldiriliar : " + ActiveEnemyAttackDebug());
                    fightManager.audioSourceTakeDamage.Play();
                }
                else
                {
                    //print("HASAR ALINDI AMA CD BITMEDI");
                }

            }


            
            if (Input.GetKeyDown(KeyCode.E))
            {
                //EnemySingleAttack();
                fightManager.FinishFight1Dodge(this);
            }
            

        }

    }

    private string ActiveEnemyAttackDebug()
    {
        string x = null;

        foreach(Vector2 vec2 in ActiveEnemyAttacks)
        {
            x = x + " X = " + vec2.x + ", Y = " + vec2.y + " /// ";
        }

        return x;
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
            //EnemySingleAttack();
            for (int i = 0; i < fight1Dodge.enemyAttackCombo; i++)
            {
                EnemyMultiAttack(Random.Range(1, fight1Dodge.MaxAttackCount));
                yield return new WaitForSecondsRealtime(fight1Dodge.enemyAttackCD);
            }

            //yield return new WaitUntil(() => isDodging)
            yield return PlayerAttackWait(); 

        }

    }

    private IEnumerator PlayerAttackWait()
    {
        float elapseTime = 0f;
        float SpaceSpamY = 0f;

        SpaceSpamObj.SetActive(true);
        while(elapseTime < fight1Dodge.PlayerAttackTime)
        {
            elapseTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpaceSpamY += -((Mathf.Sqrt(Mathf.Abs((SpaceSpamY - SpaceSpamMinVal) * SpaceSpamIncrementalPowa))) + SpaceSpamBasePowa);
                print(SpaceSpamY);
            }
            else
            {
                SpaceSpamY += Time.deltaTime * SpaceSpamDecrease;
                SpaceSpamY = Mathf.Clamp(SpaceSpamY, SpaceSpamMinVal, SpaceSpamMaxVal);
            }

            SpaceSpamObj.transform.localPosition = new Vector3(0, SpaceSpamY+SpaceSpamYOffset, 0);
            BlockData.MakePixelPerfectStatic(SpaceSpamObj.transform);

            ActiveSpaceSpamPowa = SpaceSpamY;
            //ActiveSpaceSpamPowaPercentage = OguzLib.Others.CalculatePercentage(SpaceSpamMinVal, SpaceSpamMaxVal, SpaceSpamY);
            ActiveSpaceSpamPowaPercentage = Mathf.Abs(100-OguzLib.Others.CalculatePercentage(SpaceSpamMinVal, SpaceSpamMaxVal, SpaceSpamY));
            yield return null;
        }


        fight1Dodge.HP -= PlayerStats.Instance.Damage;
        //ANIMASYON EKLICEM DUSMANA HASAR YEMESI ICIN


        SpaceSpamObj.SetActive(false);
    }

    #region SingleAttack
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

        //float elapsedTime = 0f;   // ALTTAKI IKINCI COROUTINE I  KALKDIRINCA AC BUNU


        //yield return new WaitForSecondsRealtime(fightManager.F1DTimeBeforeTakeDamageAfterAttackSpawned);  //BASKA COROUTINE E ALINDI ALTTA
        //ActiveEnemyAttacks.Add(attackDir);
        StartCoroutine(EnemySingleAttackAddList(attackDir,attackAnimFX.gameObject.GetComponent<SpriteRenderer>()));
        StartCoroutine(EnemySingleAttackRemoveList(attackDir, attackAnimFX.gameObject.GetComponent<SpriteRenderer>(), attackAnimFX));
        yield return null;

        /*
        while (elapsedTime < fight1Dodge.enemyAttackTime)
        {
            elapsedTime += Time.deltaTime;
            float animPercent = elapsedTime/fight1Dodge.enemyAttackTime;


            attackAnimFX.Play("AttackFXanim",0,animPercent);
            yield return null;
        }
        




        Destroy(attackAnimFX.gameObject);
        ActiveEnemyAttacks.Remove(attackDir);
        */
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
    private IEnumerator EnemySingleAttackRemoveList(Vector2 vector2,SpriteRenderer spriteRenderer,Animator attackAnimFX)
    {
        float elapsedTime = 0f;

        while(elapsedTime/fight1Dodge.enemyAttackTime < fightManager.F1DEnemyAttackHitPercent)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = AttackColor1;
        ActiveEnemyAttacks.Remove(vector2);

        while (elapsedTime < fight1Dodge.enemyAttackTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        Destroy(attackAnimFX.gameObject);
        //ActiveEnemyAttacks.Remove(vector2);


    }

    #endregion

    #region DoubleAttack

    private void EnemyMultiAttack(int AttackNumber)
    {
        StartCoroutine(EnemyMultiAttackCoroutine(AttackNumber));
    }

    private IEnumerator EnemyMultiAttackCoroutine(int AttackNumber)
    {
        Vector2[] attacks = OguzLib.Vectors.ReturnRandomDirections4(AttackNumber);

        foreach(Vector2 attack in attacks)
        {
            Animator attackAnimFX = Instantiate(AttackAnimPrefab, this.transform).GetComponent<Animator>();
            Vector2 attackDir = attack;
            Vector2 attackAnimDir = Vector2.zero;


            if (attackDir == Vector2.up)
            {
                attackAnimDir = fightManager.AttackAnimUp;
            }
            else if (attackDir == Vector2.down)
            {
                attackAnimDir = fightManager.AttackAnimDown;
            }
            else if (attackDir == Vector2.right)
            {
                attackAnimDir = fightManager.AttackAnimRight;
            }
            else if (attackDir == Vector2.left)
            {
                attackAnimDir = fightManager.AttackAnimLeft;
            }


            attackAnimFX.gameObject.transform.localPosition = attackAnimDir;

            StartCoroutine(EnemySingleAttackAddList(attackDir, attackAnimFX.gameObject.GetComponent<SpriteRenderer>()));
            StartCoroutine(EnemySingleAttackRemoveList(attackDir, attackAnimFX.gameObject.GetComponent<SpriteRenderer>(), attackAnimFX));
            yield return null;


        }





    }


    #endregion



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
