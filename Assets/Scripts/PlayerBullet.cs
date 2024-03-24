using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : PlayerProjectile
{
    [SerializeField] AudioClip Bullet;
    //public Rigidbody2D RigB;
    void Start()
    {
        RigB.velocity = transform.right * speed;
        Soundeff = GetComponent<AudioSource>();
        PlaySound();
    }
    void PlaySound()
    {
            Soundeff.clip = Bullet;
            Soundeff.Play();
    }
}