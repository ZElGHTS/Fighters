using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] private GameObject shield;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButton("Shield") && !Input.GetButton("Horizontal"))
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.gameObject.SetActive(false);
        }
    }
}
