using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSaw : MonoBehaviourPun
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
    //private GameObject _sawInstance;
    private Rigidbody2D _verticalSawInstance;
    //private GameObject _verticalSawInstance;
    
    private void Start()
    {
        _collider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if (Input.GetButton("Shield")) return;
        
        if ((Input.GetButton("Vertical") || Input.GetAxis("Vertical") < 0) && Input.GetButtonUp("Fire1") && _sawInstance == null && _verticalSawInstance == null)
        {
            photonView.RPC("RPC_CreateVerticalSaw", RpcTarget.AllViaServer);
            return;
        }
        
        if (Input.GetButtonUp("Fire1") && _sawInstance == null && _verticalSawInstance == null)
        {
            photonView.RPC("RPC_CreateSaw", RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    private void RPC_CreateSaw()
    {
        _sawInstance = Instantiate(sawRigidbody, (Vector2)fireTransform.position, fireTransform.rotation);
        RPC_AdjustVelocity();
        RPC_FlipSaw();

        Destroy(_sawInstance.gameObject, range);
    }

    [PunRPC]
    private void RPC_CreateVerticalSaw()
    {
        _verticalSawInstance = Instantiate(sawRigidbody, (Vector2)fireTransform.position, fireTransform.rotation);
        _verticalSawInstance.velocity = fireTransform.up * movementSpeed;
        RPC_FlipSaw();
        
        Destroy(_verticalSawInstance.gameObject, verticalRange);
    }

    private void RPC_AdjustVelocity()
    {
        if (IsPlayerGrounded())
        {
            _sawInstance.velocity = fireTransform.right * movementSpeed;
            RPC_InverseVelocity();
        }
        else
        {
            _sawInstance.velocity = -fireTransform.up * movementSpeed;
        }
    }
    
    private void RPC_FlipSaw()
    {
        sawSprite.flipX = playerTransform.localScale.x == -1;
    }
    
    private void RPC_InverseVelocity()
    {
        if (playerTransform.localScale.x == -1)
        {
            _sawInstance.velocity *= -1;
        }
    }

    private bool IsPlayerGrounded()
    {
        return Physics2D.BoxCast(_collider2D.bounds.center, _collider2D.bounds.size, 0f,
            Vector2.down, 0.1f, ground);
    }
}
