using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlippedBullet : MonoBehaviour
{
    public float Damage = 1f;
    [SerializeField] float speed = 5f;
    private Vector3 direction;
    private AudioSource Soundeff;
    public AudioClip Bullet;
    public AudioClip Ulti;
    void Start()
    {
        direction = new Vector3(-1, 0, 0);
        Soundeff = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        PlaySound();
    }

    void PlaySound()
    {
        if (Input.GetButton("Fire1"))
        {
            Soundeff.clip = Bullet;
            Soundeff.Play();
        }
        if (Input.GetButton("Fire2"))
        {
            Soundeff.clip = Ulti;
            Soundeff.Play();
        }
    }
}
