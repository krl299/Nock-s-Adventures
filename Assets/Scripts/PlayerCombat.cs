using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Components
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    [SerializeField] private Transform atkArea;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    // Layers
    private LayerMask enemyLayer;

    // Variables
    [SerializeField] private float attackRange;
    [Range(1.5f, 4f)]
    [SerializeField] private float attackSpeed;
    private float nextAtkTime = 0f;
    [Range(1, 5)]
    [SerializeField] private int atkDmg;


    private void Awake()
    {
        spriteRenderer = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        enemyLayer = LayerMask.GetMask("Enemy");
 
    }

    private void Update()
    {
        if (Time.time >= nextAtkTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Attack();
                nextAtkTime = Time.time + (1f / attackSpeed);
            }
            else if (Input.GetKeyDown(KeyCode.R) &&
                gameObject.GetComponent<PlayerMovement>().IsGrounded())
            {
                    ThrowSword();
                    nextAtkTime = Time.time + .5f;
            }
        }

        
    }

    /// <summary>
    /// 
    /// </summary>
    private void Attack()
    {
        Collider2D[] hitEnemies;

        anim.SetTrigger("atk 0");
        if (spriteRenderer.flipX == true)
            hitEnemies = Physics2D.OverlapCircleAll(atkArea.transform.position
                + new Vector3(-1.6f, 0, 0), attackRange, enemyLayer);
        else
            hitEnemies = Physics2D.OverlapCircleAll(atkArea.transform.position,
                attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().TakeDamage(atkDmg);
            Debug.Log("we hit " + enemy.name);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    private void ThrowSword()
    {
        anim.SetTrigger("throw");
        if (spriteRenderer.flipX == true)
        {
            bulletPrefab.GetComponentInChildren<SpriteRenderer>().flipX = true;
            Instantiate(bulletPrefab, firePoint.position 
                + new Vector3(-1.6f, 0, 0), firePoint.rotation);
        }
        else
        {
            bulletPrefab.GetComponentInChildren<SpriteRenderer>().flipX = false;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (atkArea == null)
            return;
        else
            Gizmos.DrawWireSphere(atkArea.transform.position, attackRange);
    }

}
