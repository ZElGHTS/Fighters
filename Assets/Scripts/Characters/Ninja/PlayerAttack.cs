using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerAttack : MonoBehaviourPun
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float strikeBoost = 20f;
    [SerializeField] private float jumpAttackHeight = 15f;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackRange = 0.6f;
    [SerializeField] private float attackDamage = 7.2f;
    [SerializeField] private Transform strikeCheck;
    [SerializeField] private float strikeRange = 0.3f;
    [SerializeField] private float strikeDamage = 28.8f;
    [SerializeField] private Transform jumpAttackCheck;
    [SerializeField] private float jumpAttackRange = 0.6f;
    [SerializeField] private float jumpAttackDamage = 11.6f;
    
    private static readonly int AttackAnimation = Animator.StringToHash("attack");
    private static readonly int StrikeAttackAnimation = Animator.StringToHash("strike");
    private static readonly int JumpAttackAnimation = Animator.StringToHash("jumpAttack");
    private Animator _animator;
    private Rigidbody2D _playerRb;
    private float _attackCooldown = 3f;
    private float _strikeCooldown = 3f;
    private float _jumpAttackCooldown = 0.9f;
    private float _nextAttackTime = 0f;
    private float _nextStrikeTime = 0f;
    private float _nextJpTime = 0f;

    public bool IsDoingHisBest { get; set; }
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if (Input.GetButton("Shield")) return;
        
        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                photonView.RPC("RPC_Attack", RpcTarget.AllViaServer);
                _nextAttackTime = Time.time + 1f / _attackCooldown;
            }
        }

        if (Time.time >= _nextStrikeTime)
        {
            if (Input.GetButtonDown("Strike"))
            {
                photonView.RPC("RPC_StrikeAttack", RpcTarget.AllViaServer);
                _nextStrikeTime = Time.time + 1f / _strikeCooldown;
            }
        }

        if (Time.time >= _nextJpTime)
        {
            if (Input.GetButtonDown("JumpAttack") || Input.GetAxis("Vertical") < 0)
            {
                photonView.RPC("RPC_JumpAttack", RpcTarget.AllViaServer);
                _nextJpTime = Time.time + 1f / _jumpAttackCooldown;
            }
        }
    }

    [PunRPC]
    private void RPC_Attack()
    {
        IsDoingHisBest = true;
        _animator.SetTrigger(AttackAnimation);
        StartCoroutine(ResetCooldown());
        
        ApplyDamage(attackCheck, attackRange, attackDamage);
    }

    [PunRPC]
    private void RPC_StrikeAttack()
    {
        IsDoingHisBest = true;
        _animator.SetTrigger(StrikeAttackAnimation);
        var movement = playerTransform.localScale.x * playerTransform.right * strikeBoost;
        _playerRb.velocity = movement;
        StartCoroutine(ResetCooldown());

        ApplyDamage(strikeCheck, strikeRange, strikeDamage);
    }

    [PunRPC]
    private void RPC_JumpAttack()
    {
        IsDoingHisBest = true;
        _animator.SetTrigger(JumpAttackAnimation);
        _playerRb.velocity = playerTransform.up * jumpAttackHeight;
        
        StartCoroutine(ResetCooldown());
        
        ApplyDamage(jumpAttackCheck, jumpAttackRange, jumpAttackDamage);
    }
    
    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(.5f);
        IsDoingHisBest = false;    
    }

    private void ApplyDamage(Transform check, float range, float damage)
    {
        var hitEnemies = Physics2D.OverlapCircleAll(check.position, range, enemyLayers);
        
        foreach (var enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerLife>().TakeDamage(damage, _playerRb.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackRange);
        Gizmos.DrawWireSphere(strikeCheck.position, strikeRange);
        Gizmos.DrawWireSphere(jumpAttackCheck.position, jumpAttackRange);
    }
}
