using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileDamage;
    [SerializeField] private bool destroyOnTouch = true;
    [SerializeField] private bool noKnockback = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerLife>().TakeDamage(projectileDamage, gameObject.transform, noKnockback);
            if (destroyOnTouch) Destroy(gameObject);
        }
    }
}
