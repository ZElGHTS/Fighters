using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected float walkingSpeed = 7f;
    [SerializeField] protected float jumpHeight = 14f;
    [SerializeField] protected LayerMask jumpAvailable;
    [SerializeField] protected GameObject jumpCheck;

    protected Rigidbody2D PlayerRb;
    protected float SideMovements;
    private Animator _animator;
    private bool _doubleJump;
    private static readonly int State = Animator.StringToHash("movementState");

    private enum MovementState
    {
        Idle, 
        Running, 
        Jumping, 
        Falling,
        DoubleJump
    }


    protected void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetPlayerDirection();
        Jump();
        UpdateAnimationState();
    }

    protected virtual void GetPlayerDirection()
    {
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            PlayerRb.velocity = new Vector2(0, jumpHeight);
            _doubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && _doubleJump)
        {
            PlayerRb.velocity = new Vector2(0, jumpHeight);
            _doubleJump = false;
        }
    }

    private void UpdateAnimationState()
    {
        MovementState movementState;
        
        if (SideMovements > 0f)
        {
            movementState = MovementState.Running;
            InverseScale(1);
        }
        else if (SideMovements < 0f)
        {
            movementState = MovementState.Running;
            InverseScale(-1);
        }
        else
        {
            movementState = MovementState.Idle;
        }

        if (PlayerRb.velocity.y > 0.1f)
        {
            movementState = MovementState.Jumping;
            if (!_doubleJump)
            {
                movementState = MovementState.DoubleJump;
            }
        }
        else if (PlayerRb.velocity.y < -0.1f)
        {
            movementState = MovementState.Falling;
        }
        
        _animator.SetInteger(State, (int)movementState);
    }

    private void InverseScale( int value)
    {
        var localScale = transform.localScale;
        localScale.x = value;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(jumpCheck.transform.position, 0.2f, jumpAvailable);
    }
}
