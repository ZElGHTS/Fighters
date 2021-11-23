using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioClip diedSoundEffect;
    
    private static readonly int Died = Animator.StringToHash("died");
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // if (!(_rigidbody2D.transform.position.y < -5f)) return;
        // Die();
        // Restart();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("trap"))
        {
            //Die();
        }
    }

    private void Die()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _animator.SetTrigger(Died);
        AudioSource.PlayClipAtPoint(diedSoundEffect, gameObject.transform.position, 1);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
