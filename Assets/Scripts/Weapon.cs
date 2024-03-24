using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform FirePoint;

    public GameObject bullet; // sets the bullet gameobject. This is what we're going to pew pew from the spaceship
    public float timeBetweenBullets = 2f; // sets the time between the bullets
    private float timeUntilNextBullet; // sets the time between each bullet

    public GameObject Ulti; // sets the bullet gameobject. This is what we're going to pew pew from the spaceship
    public float timeBetweenUltis = 5f; // sets the time between the bullets
    private float timeUntilNextUlti; // sets the time between each bullet

    private void Start()
    {
        timeUntilNextBullet = timeBetweenBullets;
        timeUntilNextUlti = timeBetweenUltis;
    }
    // Update is called once per frame
    void Update()
    {
        ShootABullet();
    }

    private void ShootABullet()
    {
        if (Input.GetButton("Fire1"))
        {
            if (timeUntilNextBullet < 0)
            {
                Shoot();
                timeUntilNextBullet = timeBetweenBullets;
            }
        }
        if (Input.GetButton("Fire2"))
        {
            if (timeUntilNextUlti < 0)
            {
                ShootUlti();
                timeUntilNextUlti = timeBetweenUltis;
            }
        }
        timeUntilNextBullet -= Time.deltaTime;
        timeUntilNextUlti -= Time.deltaTime;
    }
    private void Shoot()
    {
        GameObject X1 =Instantiate(bullet, FirePoint.position, FirePoint.rotation);
        Destroy(X1, 1f);
    }
    private void ShootUlti()
    {
        GameObject Y1 =Instantiate(Ulti, FirePoint.position, FirePoint.rotation);
        Destroy(Y1, 2f);
    }
}
