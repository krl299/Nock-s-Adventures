using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Health")]
    public int maxHealth = 8;
    public int health = 8;
    private bool canTakeDmg = true;
    public int pickUps = 0;

    [Header("Blink")]
    public float blinkSpeed = .25f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = transform.Find("PlayerSprite").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (health > maxHealth)
            health = maxHealth;
    }

    /// <summary>
    /// Control player damage
    /// </summary>
    public void TakeDamage()
    {
        if (canTakeDmg)
        {
            // reduce health
            health--;
            StartCoroutine(BlinkSprite(4));
            //StartCoroutine(nameof(BlinkSprite), 4)
            
            if (health <= 0)
            {
                GameManager.gameManager.LoseGame();
            }
        }
    }

    private IEnumerator BlinkSprite(int blinkTimes)
    {
        canTakeDmg = false;
        // repeat blinktimes
        do
        {
            spriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(blinkSpeed);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkSpeed);
            blinkTimes--;
        } while (blinkTimes > 0);
        canTakeDmg = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            //Delete the gameobject power up from the hierrarchy game
            Destroy(collision.gameObject);

            //Increment number of powerups collected
            pickUps++;

            if (pickUps >= GameManager.gameManager.pickUpCount.Length)
            {
                GameManager.gameManager.WinGame();
            }
        }
    }
}
