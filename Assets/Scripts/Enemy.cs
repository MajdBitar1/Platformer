using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp;
    public float speed;
    private PlayerProjectile Projectile;
    protected Animator EnemyAnimator;
    protected Rigidbody2D RigB;
    protected BoxCollider2D BColl;
    protected SpriteRenderer SPR;
    protected Color OGColor;
    private void Start()
    {
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(TookDamage());
            Projectile = collision.gameObject.GetComponent<PlayerProjectile>();
            Hp = Hp - Projectile.Damage;
            if (Hp <= 0)
            {
                Die();
            }
            Destroy(collision.gameObject);
        }
    }

    protected void Die()
    {
        RigB.velocity = new Vector2(0, 0);
        BColl.enabled = false;
        EnemyAnimator.SetBool("Die", true);
        Destroy(gameObject, 0.5f);
    }

    protected IEnumerator TookDamage()
    {
        SPR.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        SPR.color = OGColor;
    }
}