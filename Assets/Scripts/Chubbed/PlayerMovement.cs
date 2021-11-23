using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkingSpeed = 7f;
    [SerializeField] private float jumpHeight = 14f;
    [SerializeField] private LayerMask jumpAvailable;

    private Rigidbody2D _playerRb;
    private BoxCollider2D _collider2D;
    private Animator _animator;
    private float _sideMovements;
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

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetPlayerDirection();
        Jump();
        UpdateAnimationState();
        ExitGame();
    }

    private void GetPlayerDirection()
    {
        _sideMovements = Input.GetAxisRaw("Horizontal");
        _playerRb.velocity = new Vector2(_sideMovements * walkingSpeed, _playerRb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _playerRb.velocity = new Vector2(0, jumpHeight);
            _doubleJump = true;
        }
        else if (Input.GetButtonDown("Jump") && _doubleJump)
        {
            _playerRb.velocity = new Vector2(0, jumpHeight);
            _doubleJump = false;
        }
    }

    private void UpdateAnimationState()
    {
        MovementState movementState;
        
        if (_sideMovements > 0f)
        {
            movementState = MovementState.Running;
            InverseScale(1);
        }
        else if (_sideMovements < 0f)
        {
            movementState = MovementState.Running;
            InverseScale(-1);
        }
        else
        {
            movementState = MovementState.Idle;
        }

        if (_playerRb.velocity.y > 0.1f)
        {
            movementState = MovementState.Jumping;
            if (!_doubleJump)
            {
                movementState = MovementState.DoubleJump;
            }
        }
        else if (_playerRb.velocity.y < -0.1f)
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
        return Physics2D.BoxCast(_collider2D.bounds.center, _collider2D.bounds.size, 0f,
            Vector2.down, 0.1f, jumpAvailable);
    }

    private void ExitGame()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
