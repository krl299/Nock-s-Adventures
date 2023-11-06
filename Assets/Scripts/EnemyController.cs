using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    bat,
    walker,
    pigheaded
}

public class EnemyController : MonoBehaviour
{
    // Components
    private Animator anim;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject atkArea;
    [SerializeField] private GameObject rigthAtk;
    [SerializeField] private GameObject leftAtk;
    [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Layers
    private LayerMask groundLayer;

    //Variables
    public EnemyType enemyType;
    [SerializeField] private int maxHealth = 5;
    public int currentHealth;
    private float moveSpeed;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        moveSpeed = 3f;
    }

    private void Update()
    {
        EnemyMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("atk");
            collision.GetComponent<PlayerController>().TakeDamage();
        }
        if (collision.CompareTag("Wall"))
        {
            if (spriteRenderer.flipX == false)
                spriteRenderer.flipX = true;
            else spriteRenderer.flipX = false;
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(gameObject.transform.position, Vector2.down, 1f, groundLayer);
    }

    private void EnemyMove()
    {
        if (enemyType.Equals(EnemyType.bat))
        {
            if (spriteRenderer.flipX == true)
                rb.velocity = Vector2.left * moveSpeed;
            else
                rb.velocity = Vector2.right * moveSpeed;
        }
        else if (enemyType.Equals(EnemyType.walker) && IsGrounded())
        {
            if (spriteRenderer.flipX == true)
                rb.velocity = Vector2.left * moveSpeed;
            else
                rb.velocity = Vector2.right * moveSpeed;

        }
        else
            rb.velocity = Vector2.zero;
        
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
