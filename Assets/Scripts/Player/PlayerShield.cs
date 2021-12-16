using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerShield : MonoBehaviourPun
{
    [SerializeField] private GameObject shield;
    
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        
        if (Input.GetButton("Shield"))
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.gameObject.SetActive(false);
        }
    }
}
