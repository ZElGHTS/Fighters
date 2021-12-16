using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Saw : MonoBehaviourPun
{
    [SerializeField] private float rotationalSpeed = 2f;
    
    private void Update()
    {
        transform.Rotate(0, 0, rotationalSpeed * 360 * Time.deltaTime);
    }
}
