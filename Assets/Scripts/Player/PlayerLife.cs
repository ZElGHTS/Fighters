using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float startingPercentage = 0f;
    [SerializeField] private float basicKnockBack = 100f;
    
    private Animator _animator;
    private Rigidbody2D _playerRb;
    private float _currentPercentage;
    private float _maxBoundX = 24.5f;
    private float _minBoundX = -25.5f;
    private float _maxBoundY = 16f;
    private float _minBoundY = -16.5f;
    private float _respawnX = 0.8f;
    private float _playerRespawnY = 6f;
    private float _platformSpawnY = 5f;
    
    public void TakeDamage(float damage, Transform attacker, bool isProjectile = false)
    {
        _currentPercentage += damage;
        _animator.SetBool("hit", true);
        Debug.Log(Math.Round(_currentPercentage, 2));
        if (isProjectile) return;
        KnockBack(attacker);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody2D>();
        _currentPercentage = startingPercentage;
    }

    private void Update()
    {
        if (IsOutOfBounds())
        {
            Die();
        }
    }

    private void KnockBack(Transform attacker)
    {
        var moveDirection = _playerRb.transform.position - attacker.position;
        _playerRb.AddForce(moveDirection.normalized * basicKnockBack * _currentPercentage);
    }

    private bool IsOutOfBounds()
    {
        var position = gameObject.transform.position;
        return position.x > _maxBoundX || position.x < _minBoundX || position.y > _maxBoundY || position.y < _minBoundY;
    }

    private void Die()
    {
        Respawn();

        // other tweaks? Ex: lifes, invincible 5-10 secs, win/lose screen, etc
    }

    private void Respawn()
    {
        _currentPercentage = 0f;
        gameObject.transform.position = new Vector2(_respawnX, _playerRespawnY);
        _playerRb.bodyType = RigidbodyType2D.Static;
        _playerRb.bodyType = RigidbodyType2D.Dynamic;
        
        RespawnZone();
    }

    private void RespawnZone()
    {
        var platformInstance = Instantiate(platform, new Vector2(_respawnX, _platformSpawnY), platform.transform.rotation);
        Destroy(platformInstance, 5f);
    }

    // [SerializeField] private AudioClip diedSoundEffect;
    //
    // private static readonly int Died = Animator.StringToHash("died");
    // private Animator _animator;
    // private Rigidbody2D _rigidbody2D;
    //
    // private void Die()
    // {
    //     _rigidbody2D.bodyType = RigidbodyType2D.Static;
    //     _animator.SetTrigger(Died);
    //     AudioSource.PlayClipAtPoint(diedSoundEffect, gameObject.transform.position, 1);
    // }
}
