using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    public Detection DetectionChild;
    private GameObject Target;
    // Update is called once per frame
    private void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        RigB = GetComponent<Rigidbody2D>();
        BColl = GetComponent<BoxCollider2D>();
        SPR = GetComponent<SpriteRenderer>();
        OGColor = SPR.color;
    }
    void Update()
    {
        CheckDetection();
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Target.transform.position, speed * Time.deltaTime);
    }

    void CheckDetection()
    {
        Target = DetectionChild.Target;
    }
}
