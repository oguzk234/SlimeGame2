using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject MainCamera;
    public static PlayerMove Instance;
    private Rigidbody2D rg;
    private PlayerCollision playerCollision;

    [Header("Pixel Perfect")]
    [SerializeField] private float pixelSize = 0.0625f;

    [Header("Input")]
    public Vector2 MoveInput;
    public Vector2 MoveInputNormalized;
    public Vector3 MoveGo;

    [Header("Move")]
    [SerializeField] private float moveCooldownMax;
    [SerializeField] private float moveCooldown;
    public bool isMovable;
    public bool isMoveInputGetting;


    [Header("Animations")]
    private Animator anim;


    public enum MoveInputRot { none,right,left,up,down,rightUp,rightDown,leftUp,leftDown }
    public MoveInputRot moveInputRot;

    public Vector2 rightUp = new Vector2(1, 1);
    public Vector2 rightDown = new Vector2(1, -1);
    public Vector2 leftUp = new Vector2(-1, 1);
    public Vector2 leftDown = new Vector2(-1, -1);



    private void Awake()
    {
        Instance = this;
        rg = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<PlayerCollision>();
        anim = GetComponent<Animator>();
        pixelSize = Camera.main.orthographicSize * 2 / 216;
    }


    void Start()
    {
        
    }


    void Update()
    {
        MoveMain();
    }





    private void MoveMain()
    {
        if (!isMoveInputGetting) { return; }

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(xInput, yInput);
        MoveInputNormalized = MoveInput.normalized;


        if(MoveInput == Vector2.zero)
        {
            moveInputRot = MoveInputRot.none;
        }
        else if(MoveInput == Vector2.right)
        {
            moveInputRot = MoveInputRot.right;
        }
        else if (MoveInput == Vector2.left)
        {
            moveInputRot = MoveInputRot.left;
        }
        else if (MoveInput == Vector2.up)
        {
            moveInputRot = MoveInputRot.up;
        }
        else if (MoveInput == Vector2.down)
        {
            moveInputRot = MoveInputRot.down;
        }
        else if (MoveInput == rightUp)
        {
            moveInputRot = MoveInputRot.rightUp;
        }
        else if (MoveInput == rightDown)
        {
            moveInputRot = MoveInputRot.rightDown;
        }
        else if (MoveInput == leftUp)
        {
            moveInputRot = MoveInputRot.leftUp;
        }
        else if (MoveInput == leftDown)
        {
            moveInputRot = MoveInputRot.leftDown;
        }

        MoveGo = Vector3.zero;

        if (!isMovable) { return; }


        moveCooldown -= Time.deltaTime;

        if(moveCooldown <= 0 && MoveInput != Vector2.zero)
        {
            MoveGo = (Vector3)MoveInput * pixelSize;
            MoveGo = playerCollision.CheckFutureMoveCollision(MoveGo);

            this.gameObject.transform.position += MoveGo;
            //rg.MovePosition(rg.position += (Vector2)MoveGo);
            
            if (isMoveInputSingular()) { moveCooldown = moveCooldownMax; }
            else { moveCooldown = moveCooldownMax * Mathf.Sqrt(2); }
            
        }

    }
    public bool isMoveInputSingular()
    {
        if(MoveInput.x != 0 && MoveInput.y != 0) { return false; } 
        return true;
    }




}
