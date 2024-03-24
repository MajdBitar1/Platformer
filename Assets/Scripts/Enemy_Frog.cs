using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private GameObject Target;
    [SerializeField] Detection DetectionChild;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        RigB = GetComponent<Rigidbody2D>();
        BColl = GetComponent<BoxCollider2D>();
        SPR = GetComponent<SpriteRenderer>();
        OGColor = SPR.color;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetection();
        if ( gameObject.transform.position != Target.transform.position)
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Target.transform.position, speed * Time.deltaTime);
    }
    void CheckDetection()
    {
        Target = DetectionChild.Target;
    }
}
