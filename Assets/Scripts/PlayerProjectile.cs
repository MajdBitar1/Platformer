using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float Damage;
    public float speed; // this is the bullet's speed
    public Rigidbody2D RigB;
    protected AudioSource Soundeff;

    private void Start()
    {
        RigB = GetComponent<Rigidbody2D>();
    }
}
