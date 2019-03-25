using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    #region Event trigger
    public enum ButtonAction { MoveLeft, MoveRight, Jump, Shoot }
    public enum ButtonState { Down, Up }
    public enum State { Normal, Die }

    [HideInInspector]
    public bool facingRight = true;         
    [HideInInspector]
    public bool jump = false;

    internal bool m_leftButton = false;
    internal bool m_rightButton = false;
    internal bool m_jumpButton = false;
    internal bool m_jumpButtonReleased = false;
    #endregion
    const float COOLDOWN_JUMP = 0.1f;
    const int MAX_LIFEPOINT = 5;

    public GameObject CheckPoint;
    public GameManager _GameManager;
    public GameObject Normal;
    public GameObject Big;
    public GameObject Die;
    //public FindGrounded Find;
    public float healthPoint = 500f;
    public int lifePoint = 3;
    public float runSpeed = 10f;
    public float jumpForce = 1000f;
    //public float ExtraPowerJump = 500f;
    
    public State StartState = State.Normal;
    public Animator animator;
    private Rigidbody2D R2D;
    private Transform groundCheck;
    private bool isGround = false;
    private bool isShooting = false;

    //public PlayerController(int hp, int m, State state) : base(hp, m)
    //{
    //    state = State.Normal;
    //}
    void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
    }
    // Start is called before the first frame update
    void Start()
    {
        R2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(R2D.velocity.x));
        animator.SetFloat("VerticalVelocity", R2D.velocity.y);
        animator.SetBool("isShooting", isShooting);
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && isGround)
        {
            jump = true;
            //animator.SetBool("isJumping", true);
        }
        if(Input.GetButton("Fire1"))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }

    private void FixedUpdate()
    {
        HandleInput();
    }

    void HandleInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        if (h == 1)
        {
            
        }
        
        
        //    if (Input.GetKey(KeyCode.LeftArrow) || m_leftButton)
        //    {
        //        if (m_Move != -1)
        //        {
        //            m_R2D.angularVelocity = 0;
        //            m_R2D.velocity = new Vector2(0, m_R2D.velocity.y);
        //        }
        //        m_Move = -1;
        //    }

        //    else if (Input.GetKey(KeyCode.RightArrow) || m_rightButton)
        //    {
        //        if (m_Move != 1)
        //        {
        //            m_R2D.angularVelocity = 0;
        //            m_R2D.velocity = new Vector2(0, m_R2D.velocity.y);
        //        }
        //        m_Move = 1;
        //    }
        //    else
        //    {
        //        if (m_Move != 0)
        //        {
        //            m_R2D.angularVelocity = 0;
        //            m_R2D.velocity = new Vector2(0, m_R2D.velocity.y);
        //        }
        //        m_Move = 0;
        //    }
        //    if ((Input.GetKey(KeyCode.Space) || m_jumpButton) && Find.IsGround &&
        //       (m_JumpTime > COOLDOWN_JUMP && m_State != State.Die))
        //        Jump();
        //    if ((Input.GetKeyUp(KeyCode.Space) || m_jumpButtonReleased))
        //    {
        //        m_IsPowerGravity = false;
        //        if (!m_IsAcsimet)
        //            m_R2D.gravityScale = 1;

        //    }

        //    m_jumpButtonReleased = false;



        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            // Add a vertical force to the player.
            R2D.AddForce(new Vector2(0f, jumpForce));
            
            // Make sure the player can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
        if(R2D.velocity.y != 0)
            h = h * 0.5f;
        R2D.velocity = new Vector2(h * runSpeed, R2D.velocity.y);
    }
    //public void OnLanding()
    //{
    //    animator.SetBool("isJumping", false);
    //}

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
