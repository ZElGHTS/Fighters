using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private PlatformEffector2D _effector2D;
    private float _waitTime;
    
    private void Start()
    {
        _effector2D = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        GoThroughPlatform();
    }

    private void GoThroughPlatform()
    {
        if (Input.GetButtonUp("Vertical") && Input.GetAxisRaw("Vertical") >= 0)
        {
            _waitTime = 0.5f;
        }

        if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            if (_waitTime <= 0)
            {
                _effector2D.rotationalOffset = 180f;
                _waitTime = 0.5f;
            }
            else
            {
                _waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetButton("Jump"))
        {
            _effector2D.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
