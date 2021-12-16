using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerFireball : MonoBehaviourPun
{
    [SerializeField] private Rigidbody2D fireball;
    [SerializeField] private SpriteRenderer fireballSprite;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float fireballSpeed = 15f;
    [SerializeField] private float fireballRange = 0.7f;

    private float _nextFireballTime = 0f;
    private float _fireballCooldown = 2f;

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if (Input.GetButton("Shield")) return;
        
        if (Time.time >= _nextFireballTime)
        {
            if (Input.GetButtonUp("Shift"))
            {
                photonView.RPC("RPC_Fire", RpcTarget.AllViaServer);
                _nextFireballTime = Time.time + 1f / _fireballCooldown;
            }
        }
    }

    [PunRPC]
    private void RPC_Fire()
    {
        var fireballInstance = Instantiate(fireball, (Vector2)fireTransform.position, fireTransform.rotation);
        fireballInstance.velocity = fireTransform.right * fireballSpeed;
        RPC_InverseVelocity(fireballInstance);
        RPC_FlipFireball();

        Destroy(fireballInstance.gameObject, fireballRange);
    }

    private void RPC_InverseVelocity(Rigidbody2D instance)
    {
        if (playerTransform.localScale.x == -1)
        {
            instance.velocity *= -1;
        }
    }

    private void RPC_FlipFireball()
    {
        fireballSprite.flipX = playerTransform.localScale.x == -1;
    }
}
