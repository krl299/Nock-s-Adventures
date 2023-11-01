using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField] private GameObject impactEffect;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (sprite.flipX)
            rb.velocity = -transform.right * bulletSpeed;
        else
            rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().TakeDamage(2);
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }  
        else if (collision.CompareTag("Ground"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        
        
    }

}
