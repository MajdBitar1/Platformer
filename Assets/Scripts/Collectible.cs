using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] GameObject ClaimEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.score++;
            GameManager.instance.scoreText.text = GameManager.instance.score.ToString();//ScoreText is a String so to convert Score which is INT to String we Use ToString Function
            LevelManager.instance.RemovePosition(this);
            Destroy(gameObject);
            Instantiate(ClaimEffect, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
