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
    private float atkRange;
    


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        enemyLayer = GetComponentInChildren<LayerMask>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;    
    }

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
