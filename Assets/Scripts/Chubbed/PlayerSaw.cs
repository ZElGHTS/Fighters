using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaw : MonoBehaviour
{
    [SerializeField] private Rigidbody2D sawRigidbody;
    [SerializeField] private SpriteRenderer sawSprite;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float movementSpeed = 15f;
    [SerializeField] private float range = 2f;
    [SerializeField] private float verticalRange = 0.3f;

    private BoxCollider2D _collider2D;
    private Rigidbody2D _sawInstance;
    private Rigidbody2D _verticalSawInstance;
    
    private void Start()
    {
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        FlipSaw();
        if ((Input.GetButton("Vertical") && Input.GetButtonUp("Fire1")) && _sawInstance == null && _verticalSawInstance == null)
        {
            CreateVerticalSaw();
            return;
        }
        
        if (Input.GetButtonUp("Fire1") && _sawInstance == null && _verticalSawInstance == null)
        {
            CreateSaw();
        }
    }

    private void FlipSaw()
    {
        sawSprite.flipX = playerTransform.localScale.x == -1;
    }
    
    private void CreateSaw()
    {
        _sawInstance = Instantiate(sawRigidbody, (Vector2)fireTransform.position, fireTransform.rotation);
        adjustVelocity();

        Destroy(_sawInstance.gameObject, range);
    }

    private void CreateVerticalSaw()
    {
        _verticalSawInstance = Instantiate(sawRigidbody, (Vector2)fireTransform.position, fireTransform.rotation);
        _verticalSawInstance.velocity = fireTransform.up * movementSpeed;
        
        Destroy(_verticalSawInstance.gameObject, verticalRange);
    }

    private void adjustVelocity()
    {
        if (isPlayerGrounded())
        {
            _sawInstance.velocity = fireTransform.right * movementSpeed;
            InverseVelocity();
        }
        else
        {
            _sawInstance.velocity = -fireTransform.up * movementSpeed;
        }
    }
    
    private void InverseVelocity()
    {
        if (playerTransform.localScale.x == -1)
        {
            _sawInstance.velocity *= -1;
        }
    }

    private bool isPlayerGrounded()
    {
        return Physics2D.BoxCast(_collider2D.bounds.center, _collider2D.bounds.size, 0f,
            Vector2.down, 0.1f, ground);
    }
}
