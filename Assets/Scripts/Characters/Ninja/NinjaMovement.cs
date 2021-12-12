using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaMovement : PlayerMovement
{
    private PlayerAttack _playerAttack;

    protected new void Start()
    {
        base.Start();
        _playerAttack = GetComponent<PlayerAttack>();
    }

    protected override void GetPlayerDirection()
    {
        SideMovements = Input.GetAxisRaw("Horizontal");
        if (SideMovements != 0f || !_playerAttack.IsDoingHisBest) 
        {
            PlayerRb.velocity = new Vector2(SideMovements * walkingSpeed, PlayerRb.velocity.y);
        }
    }
}
