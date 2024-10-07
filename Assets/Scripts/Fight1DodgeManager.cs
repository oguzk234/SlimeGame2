using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight1DodgeManager : MonoBehaviour
{
    public GameObject MCFight;
    public GameObject EnemyFight;
    public SpriteRenderer EnemyRenderer;
    public Fight1Dodge fight1Dodge;

    public bool isFightStarted;


    [Header("Dodge Setting")]
    public float dodgeTime;
    public float dodgePercent; //ELLEME EDITORDE

    public Vector2 ActiveDodgeDir;

    public AnimationCurve DodgeAnimCurve;
    public float DodgeRange;

    public bool isDodging;


    private void Start()
    {
        //MCFight = FightManager.Instance.MCFight1Dodge;
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
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
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
    }



    private void Dodge(Vector2 dodgeDir)
    {
        if (isDodging == true) { return; }

        StartCoroutine(DodgeCoroutine(dodgeDir));
    }
    private IEnumerator DodgeCoroutine(Vector2 dodgeDir)
    {
        isDodging = true;
        
        float elapsedTime = 0f;

        ActiveDodgeDir = dodgeDir;

        while (elapsedTime < dodgeTime)
        {
            elapsedTime += Time.deltaTime;

            dodgePercent = DodgeAnimCurve.Evaluate(elapsedTime / dodgeTime);
            //dodgePercent = (elapsedTime / dodgeTime);
            //float animationPercent = DodgeAnimCurve.Evaluate(dodgeTime);

            MCFight.transform.localPosition = new Vector3(ActiveDodgeDir.x*dodgePercent * DodgeRange, ActiveDodgeDir.y * dodgePercent * DodgeRange, MCFight.transform.position.z);
            BlockData.MakePixelPerfectStatic(MCFight.transform);
            
            yield return null;
        }

        ActiveDodgeDir = Vector2.zero;

        Debug.Log("Dodgjj tamamlandý!");

        isDodging = false;
    }




}
