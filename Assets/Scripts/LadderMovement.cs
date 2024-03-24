using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float speed = 10f;
    private bool isLadder;
    private Animator PlayerAnimator;

    private Rigidbody2D RB;
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (isLadder)
        {
            PlayerAnimator.SetBool("Climbing", true);
            RB.gravityScale = 0;
            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
            }
        }
        else
        {
            PlayerAnimator.SetBool("Climbing", false);
            RB.gravityScale = 10;
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
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }
}