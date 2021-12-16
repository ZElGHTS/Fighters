using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerKunai : MonoBehaviourPun
{
    [SerializeField] private Rigidbody2D kunai;
    [SerializeField] private SpriteRenderer kunaiSprite;
    [SerializeField] private Transform throwTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float kunaiSpeed = 15f;
    [SerializeField] private float kunaiRange = 0.7f;

    private float _nextKunaiTime = 0f;
    private float _kunaiCooldown = 2f;

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if (Input.GetButton("Shield")) return;
        
        if (Time.time >= _nextKunaiTime)
        {
            if (Input.GetButtonUp("Shift"))
            {
                photonView.RPC("RPC_Throw", RpcTarget.AllViaServer);
                _nextKunaiTime = Time.time + 1f / _kunaiCooldown;
            }
        }
    }

    [PunRPC]
    private void RPC_Throw()
    {
        var kunaiInstance = Instantiate(kunai, (Vector2)throwTransform.position, throwTransform.rotation);
        kunaiInstance.velocity = throwTransform.right * kunaiSpeed;
        RPC_InverseVelocity(kunaiInstance);
        RPC_FlipKunai();

        Destroy(kunaiInstance.gameObject, kunaiRange);
    }

    private void RPC_InverseVelocity(Rigidbody2D instance)
    {
        if (playerTransform.localScale.x == -1)
        {
            instance.velocity *= -1;
        }
    }

    private void RPC_FlipKunai()
    {
        kunaiSprite.flipX = playerTransform.localScale.x != -1;
    }
}
