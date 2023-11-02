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

    // Layers
    private LayerMask enemyLayer;

    //Variables
    [SerializeField] private int maxHealth = 5;
    public int currentHealth;
<<<<<<< HEAD
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    [SerializeField] private GameObject rigthAtk;
    [SerializeField] private GameObject leftAtk;
    private LayerMask playerLayer;
=======
    private float atkRange;
    

>>>>>>> 7259ca5dee01f450471d2294aceaa560a58883dd

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
<<<<<<< HEAD
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerLayer = GetComponentInChildren<LayerMask>();      
=======
        enemyLayer = GetComponentInChildren<LayerMask>();
>>>>>>> 7259ca5dee01f450471d2294aceaa560a58883dd
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;    
    }

<<<<<<< HEAD
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("atk");
            collision.GetComponent<PlayerController>().TakeDamage();
        }
    }

=======
    private void Update()
    {
        if (HasTarget())
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    private bool HasTarget()
    {
        return Physics2D.Raycast(atkArea.transform.position, Vector2.left, 0.5f, enemyLayer);
    }
    private void OnDrawGizmosSelected()
    {
        if (atkArea == null)
            return;
        else
            Gizmos.DrawLine(atkArea.transform.position, new Vector2(-0.5f, 0f));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
>>>>>>> 7259ca5dee01f450471d2294aceaa560a58883dd
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("hit");
        if (currentHealth <= 0)
            Die();

    }

    /// <summary>
    /// 
    /// </summary>
    private void Attack()
    {
        

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
