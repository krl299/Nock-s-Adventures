using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    // Components
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private GameObject feet;
    [SerializeField]private Transform atkRightArea;
    [SerializeField]private Transform atkLeftArea;
    private Animator anim;

    // Layers
    private LayerMask groundLayer;
    private LayerMask wallLayer;
    private LayerMask enemyLayer;

    // Enums
    private enum movementState { idle, running, jumping, falling }

    // Variables
    [SerializeField] private float dirX;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int multipleJumps;
    [SerializeField] private static int countJumps = 0;
    [SerializeField] private float attackRange;

    #endregion

    // Awake is called when loading the script instance
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>();
        feet = GameObject.Find("PlayerFeet");
        anim = GetComponentInChildren<Animator>();
        groundLayer = LayerMask.GetMask("Ground");
        wallLayer = LayerMask.GetMask("Wall");
        enemyLayer = LayerMask.GetMask("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2 (dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            ResetJump();
            if (!IsGrounded() && IsOnRightWall())
            {
                Jump(new Vector2(-1, 1), 1);
            }
            else
            {
                if (countJumps < (multipleJumps + 1) && countJumps >= 0)
                    countJumps++;
                Debug.Log(countJumps);
                Jump(Vector2.up, multipleJumps);
            }                                   
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }

        UpdateAnimationState();

    }

   

    #region Methods

    /// <summary>
    /// Check if the player is on ground or not.
    /// </summary>
    private bool IsGrounded()
    {
        return Physics2D.Raycast(feet.transform.position, Vector2.down, .1f, groundLayer);
    }
    
    // TODO Jump on walls
    private bool IsOnRightWall()
    {
        if (/*Physics2D.Raycast(feet.transform.position, Vector2.left, .9f, wallLayer)
            || */Physics2D.Raycast(feet.transform.position, Vector2.right, .09f, wallLayer))
        {
            Debug.Log("onWall");
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Jump(Vector2 up, int maxJumps)
    {
        if (countJumps == maxJumps+1)
            return ;
        rb.velocity = up * (jumpForce /*/ countJumps*/);
    }

    /// <summary>
    /// 
    /// </summary>
    private void ResetJump()
    {
        if (IsGrounded())
            countJumps = 0;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Attack()
    {
        Collider2D[] hitEnemies;

        anim.SetTrigger("atk 0");
        if (spriteRenderer.flipX == true)
            hitEnemies =  Physics2D.OverlapCircleAll(atkRightArea.transform.position
                + new Vector3(-1.6f, 0, 0) , attackRange, wallLayer);
        else
            hitEnemies = Physics2D.OverlapCircleAll(atkRightArea.transform.position,
                attackRange, wallLayer);

        foreach (Collider2D hit in hitEnemies)
        {
            //TODO hit Enemies
            Debug.Log("we hit enemies");
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(atkRightArea.transform.position, attackRange);
        //Gizmos.DrawWireSphere(atkLeftArea.transform.position, attackRange);
    }

    /// <summary>
    /// Update animations
    /// </summary>
    private void UpdateAnimationState()
    {
        movementState state;

        if (dirX < 0)
            spriteRenderer.flipX = true;
        else if (dirX > 0)
            spriteRenderer.flipX = false;

        state = (dirX != 0) ? movementState.running : movementState.idle;

        if (rb.velocity.y > .1f)
            state = movementState.jumping;
        else if (rb.velocity.y < -.1f)
            state = movementState.falling;

        anim.SetInteger("state", (int)state);
    }

    #endregion
}
