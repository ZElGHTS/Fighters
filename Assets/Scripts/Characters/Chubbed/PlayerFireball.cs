using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D fireball;
    [SerializeField] private SpriteRenderer fireballSprite;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float fireballSpeed = 15f;
    [SerializeField] private float fireballRange = 0.7f;

    private float _nextFireballTime = 0f;
    private float _fireballCooldown = 2f;

    private void Update()
    {
        if (Input.GetButton("Shield")) return;
        
        FlipFireball();
        if (Time.time >= _nextFireballTime)
        {
            if (Input.GetButtonUp("Shift"))
            {
                Fire();
                _nextFireballTime = Time.time + 1f / _fireballCooldown;
            }
        }
    }

    private void Fire()
    {
        var fireballInstance = Instantiate(fireball, (Vector2)fireTransform.position, fireTransform.rotation);
        fireballInstance.velocity = fireTransform.right * fireballSpeed;
        InverseVelocity(fireballInstance);

        Destroy(fireballInstance.gameObject, fireballRange);
    }

    private void InverseVelocity(Rigidbody2D instance)
    {
        if (playerTransform.localScale.x == -1)
        {
            instance.velocity *= -1;
        }
    }

    private void FlipFireball()
    {
        fireballSprite.flipX = playerTransform.localScale.x == -1;
    }
}
