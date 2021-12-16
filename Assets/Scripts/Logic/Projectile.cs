using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Projectile : MonoBehaviourPun
{
    [SerializeField] private float projectileDamage;
    [SerializeField] private bool destroyOnTouch = true;
    [SerializeField] private bool noKnockback = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PhotonView>().GetComponent<PlayerLife>().TakeDamage(projectileDamage, gameObject.transform, noKnockback);
            if (destroyOnTouch)
            {
                Destroy(gameObject);
            }
        }
    }
}
