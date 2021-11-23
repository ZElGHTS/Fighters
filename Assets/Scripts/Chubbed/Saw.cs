using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float rotationalSpeed = 2f;
    
    private void Update()
    {
        transform.Rotate(0, 0, rotationalSpeed * 360 * Time.deltaTime);
    }
}
