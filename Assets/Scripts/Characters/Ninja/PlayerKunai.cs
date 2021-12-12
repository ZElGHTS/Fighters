using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKunai : MonoBehaviour
{
    [SerializeField] private Rigidbody2D kunai;
    [SerializeField] private SpriteRenderer kunaiSprite;
    [SerializeField] private Transform throwTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float kunaiSpeed = 15f;
    [SerializeField] private float kunaiRange = 0.7f;

    private float _nextKunaiTime = 0f;
    private float _kunaiCooldown = 2f;

    private void Update()
    {
        if (Input.GetButton("Shield")) return;
        
        FlipKunai();
        if (Time.time >= _nextKunaiTime)
        {
            if (Input.GetButtonUp("Shift"))
            {
                Throw();
                _nextKunaiTime = Time.time + 1f / _kunaiCooldown;
            }
        }
    }

    private void Throw()
    {
        var kunaiInstance = Instantiate(kunai, (Vector2)throwTransform.position, throwTransform.rotation);
        kunaiInstance.velocity = throwTransform.right * kunaiSpeed;
        InverseVelocity(kunaiInstance);

        Destroy(kunaiInstance.gameObject, kunaiRange);
    }

    private void InverseVelocity(Rigidbody2D instance)
    {
        if (playerTransform.localScale.x == -1)
        {
            instance.velocity *= -1;
        }
    }

    private void FlipKunai()
    {
        kunaiSprite.flipX = playerTransform.localScale.x != -1;
    }
}
