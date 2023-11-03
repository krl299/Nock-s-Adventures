using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Components
    private Animator anim;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject atkArea;

    //Variables
    [SerializeField] private int maxHealth = 5;
    public int currentHealth;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject rigthAtk;
    [SerializeField] private GameObject leftAtk;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("atk");
            collision.GetComponent<PlayerController>().TakeDamage();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("hit");
        if (currentHealth <= 0)
            Die();

    }

    void Die()
    {
        Debug.Log("enemy die.");
        // Die animation
        anim.SetBool("IsDead", true);

        //Disable the enemy
        gameObject.SetActive(false);
        
    }
}
