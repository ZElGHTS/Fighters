using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChubbedMovement : PlayerMovement
{
    protected override void GetPlayerDirection()
    {
        SideMovements = Input.GetAxisRaw("Horizontal");
        PlayerRb.velocity = new Vector2(SideMovements * walkingSpeed, PlayerRb.velocity.y);
    }
}
