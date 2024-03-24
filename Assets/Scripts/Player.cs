using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour
{
    private Rigidbody2D RigB;
    private Animator PlayerAnimator;
    private bool isMovingRight = true;
    private bool isGrounded = true;
    private float LastGroundedTime=0;
    private float LastJumpTime=0;
    public float CoyoteTime;
    private bool isLadder;
    private CapsuleCollider2D CapCollider2D;
    private bool Crouched = false;
    private bool isAlive = true;
    [SerializeField] float speed;
    [SerializeField] float LadderSpeedMulti;
    [SerializeField] float JumpValue;
    [SerializeField] float MaxSpeedHoriz;
    [SerializeField] float MaxSpeedVert;
    [SerializeField] CinemachineVirtualCamera Vcam1; //Following
    [SerializeField] CinemachineVirtualCamera Vcam2; //Death
    [SerializeField] GroundDetection GroundDetector;

    void Start()
    {
        Vcam1.Priority = 1;
        Vcam2.Priority = 0;
        RigB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        CapCollider2D = GetComponent<CapsuleCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            Run();
            isCrouching();
            Parameters();
            ClimbingLadder();
            Jump();
        }
    }
    private void FixedUpdate()
    {
        //ClimbingLadder();
    }

    private void Run()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (horizontalMovement > 0 && !isMovingRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0 && isMovingRight)
        {
            Flip();
        }
        else if (horizontalMovement == 0)
            RigB.velocity = new Vector2(0, RigB.velocity.y);

        float moveBy = horizontalMovement * speed;
        if (Mathf.Abs(RigB.velocity.x) > MaxSpeedHoriz)
            RigB.velocity = new Vector2(MaxSpeedHoriz * horizontalMovement, RigB.velocity.y);
        else
            RigB.AddForce(new Vector2(moveBy, 0), ForceMode2D.Force);

        if (isGrounded)
        {
            PlayerAnimator.SetFloat("Horizontal Velocity", Mathf.Abs(moveBy));
        }
        else
        {
            PlayerAnimator.SetFloat("Vertical Velocity", RigB.velocity.y);
        }
    }
    private void Flip()
    {
        RigB.velocity = new Vector2(0, RigB.velocity.y);
        isMovingRight = !isMovingRight;
        gameObject.GetComponent<Player>().transform.Rotate(0f, 180f, 0f);
    }
    private void isCrouching()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            Crouched = true;
            gameObject.transform.localScale = gameObject.transform.localScale * 0.65f;
            //CapCollider2D.size = CapCollider2D.size * 0.65f;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            Crouched = false;
            gameObject.transform.localScale = gameObject.transform.localScale * (1/0.65f);
            //CapCollider2D.size = CapCollider2D.size * (1 / 0.65f);
        }
    }
    private void Grounded()
    {
        isGrounded = GroundDetector.Grounded;
        PlayerAnimator.SetBool("Grounded", isGrounded);
        if (isGrounded)
        {
            LastGroundedTime = CoyoteTime;
            LastJumpTime = CoyoteTime;
        }
    }
    private void Jump()
    {
        Grounded();
        LastJumpTime -= Time.deltaTime;
        LastGroundedTime -= Time.deltaTime;
        if (Input.GetButtonDown("Jump") && LastGroundedTime > 0 && LastJumpTime > 0 && isGrounded && !isLadder)
        {
            RigB.AddForce(Vector2.up * JumpValue, ForceMode2D.Impulse);
            isGrounded = false;
            LastJumpTime = CoyoteTime;
        }
    }
    private void Parameters()
    {
        PlayerAnimator.SetBool("Jump", !isGrounded);
        PlayerAnimator.SetBool("Crouch", Crouched);
        PlayerAnimator.SetBool("Ladder", isLadder);
    }
    private void ClimbingLadder()
    {
        if (isLadder)
        {
            PlayerAnimator.SetBool("Ladder", true);
            RigB.gravityScale = 0;
            if (Input.GetButton("Vertical") )
            {
                RigB.AddForce(Vector2.up * Input.GetAxisRaw("Vertical") * LadderSpeedMulti, ForceMode2D.Impulse);
            }
        }
        else
        {
            PlayerAnimator.SetBool("Ladder", false);
            RigB.gravityScale = 10;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DropDown"))
        {
            if(Crouched )
                collision.gameObject.GetComponent<CompositeCollider2D>().isTrigger = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            int livesleft = GameManager.instance.LivesLeft;
            if (livesleft > 0)
            {
                GameManager.instance.LivesOnScreen[livesleft - 1].gameObject.SetActive(false);
                GameManager.instance.LivesLeft--;
            }
            else if (livesleft == 0)
            {
                Destroy(LevelManager.instance.gameObject);
                Destroy(GameManager.instance.gameObject);
            }
            StartCoroutine(Die());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("DropDown") == true)
        {
            collision.gameObject.GetComponent<CompositeCollider2D>().isTrigger = false;
        }
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
    private IEnumerator Die()
    {
        Vcam1.Priority = 0;
        Vcam2.Priority = 1;
        isAlive = false;
        RigB.velocity = new Vector2(RigB.velocity.x, 30);
        PlayerAnimator.SetBool("Dead", true);
        CapCollider2D.enabled = false;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
