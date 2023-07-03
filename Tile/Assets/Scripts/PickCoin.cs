using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCoin : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] int points = 100;

    bool wasCollected;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(points);
            Destroy(gameObject);
        }
    }
}
