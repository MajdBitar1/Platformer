using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Opossum : Enemy
{
    public GameObject[] MovementPositions;
    private int currentMovementPosition=0;
    private bool isMovingRight = false;
    private void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        RigB = GetComponent<Rigidbody2D>();
        BColl = GetComponent<BoxCollider2D>();
        SPR = GetComponent<SpriteRenderer>();
        OGColor = SPR.color;
    }
    private void Update()
    {
        Walk();
    }
    private void Walk()
    {
        //FLIPPING
        if (MovementPositions[currentMovementPosition].transform.position.x > gameObject.transform.position.x && !isMovingRight)
        {
            Flip();
        }
        else if (MovementPositions[currentMovementPosition].transform.position.x < gameObject.transform.position.x && isMovingRight)
        {
            Flip();
        }
        //FLIPPING END
        //MOVING
        if (Vector2.Distance(gameObject.transform.position, MovementPositions[currentMovementPosition].transform.position) > 0.5f)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, MovementPositions[currentMovementPosition].transform.position, speed * Time.deltaTime);
        }
        else
        {
            currentMovementPosition = currentMovementPosition + 1;
            if (currentMovementPosition >= MovementPositions.Length)
                currentMovementPosition = 0;
        }
    }

    private void Flip()
    {
        isMovingRight = !isMovingRight;
        gameObject.GetComponent<Enemy_Opossum>().transform.Rotate(0f, 180f, 0f);
    }
}
