using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        FollowPlayer();
        //Pause();
    }

    private void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    private void Pause()
    {
        while (Input.GetButtonDown("Submit"))
        {
            //Time.timeScale = 0;
        }
        //Time.timeScale = 1;
    }
}
