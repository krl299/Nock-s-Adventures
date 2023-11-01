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
    private Animator anim;

    // Layers
    private LayerMask groundLayer;
    private LayerMask wallLayer;

    // Enums
    private enum movementState { idle, running, jumping, falling }

    // Variables
    private float dirX;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int multipleJumps;
    [SerializeField] private static int countJumps = 0;
    

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
                Debug.Log("jump " + countJumps);
                Jump(Vector2.up, multipleJumps);
            }                                   
        }

        UpdateAnimationState();

    }

    #region Methods

    /// <summary>
    /// Check if the player is on ground or not.
    /// </summary>
    public bool IsGrounded()
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
