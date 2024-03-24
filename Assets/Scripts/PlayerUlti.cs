using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUlti : PlayerProjectile
{
    [SerializeField] AudioClip UltiEff;
    //public Rigidbody2D RigB;
    void Start()
    {
        RigB.velocity = transform.right * speed;
        Soundeff = GetComponent<AudioSource>();
        PlaySound();
    }
    void PlaySound()
    {
            Soundeff.clip = UltiEff;
            Soundeff.Play();
    }
}
