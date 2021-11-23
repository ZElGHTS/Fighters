using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 2f;

    private int _currentWaypoint = 0;
    
    private void Update()
    {
        if (Vector2.Distance(waypoints[_currentWaypoint].transform.position, transform.position) < 0.1f)
        {
            _currentWaypoint++;
            if (_currentWaypoint >= waypoints.Length)
            {
                _currentWaypoint = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, 
            waypoints[_currentWaypoint].transform.position, Time.deltaTime * speed);
    }
}
