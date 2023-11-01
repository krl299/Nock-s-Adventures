using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    public int currentHealth;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;    
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
