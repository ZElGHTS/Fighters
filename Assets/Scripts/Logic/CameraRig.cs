using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] private CameraFocus focus;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private float depthUpdateSpeed = 5f;
    [SerializeField] private float angleUpdateSpeed = 7f;
    [SerializeField] private float positionUpdateSpeed = 5f;
    [SerializeField] private float depthMax = -10f;
    [SerializeField] private float depthMin = 3; // -22
    [SerializeField] private float angleMax = 11f;
    [SerializeField] private float angleMin = 3f;

    private float _cameraEulerX;
    private Vector3 _cameraPosition;

    private void Start()
    {
        players.Add(focus.gameObject);
        players.Add(QuickInstantiate.Prefab);
    }

    private void LateUpdate()
    {
        CalculateCameraLocations();
        MoveCamera();
    }

    private void MoveCamera()
    {
        var position = gameObject.transform.position;
        if (position != _cameraPosition)
        {
            var newPosition = Vector3.zero;
            newPosition.x = Mathf.MoveTowards(position.x, _cameraPosition.x, positionUpdateSpeed * Time.deltaTime);
            newPosition.y = Mathf.MoveTowards(position.y, _cameraPosition.y, positionUpdateSpeed * Time.deltaTime);
            newPosition.z = Mathf.MoveTowards(position.z, _cameraPosition.z, depthUpdateSpeed * Time.deltaTime);

            gameObject.transform.position = newPosition;
        }

        var localEulerAngles = gameObject.transform.localEulerAngles;
        if (localEulerAngles.x != _cameraEulerX)
        {
            var newEulerAngles = new Vector3(_cameraEulerX, localEulerAngles.y, localEulerAngles.z);
            gameObject.transform.localEulerAngles =
                Vector3.MoveTowards(localEulerAngles, newEulerAngles, angleUpdateSpeed * Time.deltaTime);
        }
    }

    private void CalculateCameraLocations()
    {
        var averageCenter = Vector3.zero;
        var totalPositions = Vector3.zero;
        var playerBounds = new Bounds();

        foreach (var player in players)
        {
            var playerPosition = player.transform.position;

            if (!focus.focusBounds.Contains(playerPosition))
            {
                var playerX = Mathf.Clamp(playerPosition.x, focus.focusBounds.min.x, focus.focusBounds.max.x);
                var playerY = Mathf.Clamp(playerPosition.y, focus.focusBounds.min.y, focus.focusBounds.max.y);
                var playerZ = Mathf.Clamp(playerPosition.z, focus.focusBounds.min.z, focus.focusBounds.max.z);
                playerPosition = new Vector3(playerX, playerY, playerZ);
            }

            totalPositions += playerPosition;
            playerBounds.Encapsulate(playerPosition);
        }

        averageCenter = totalPositions / players.Count;

        var extents = playerBounds.extents.x + playerBounds.extents.y;
        var lerpPercent = Mathf.InverseLerp(0, focus.halfBoundsX + focus.halfBoundsY, extents);

        var depth = Mathf.Lerp(depthMax, depthMin, lerpPercent);
        var angle = Mathf.Lerp(angleMax, angleMin, lerpPercent);

        _cameraEulerX = angle;
        _cameraPosition = new Vector3(averageCenter.x, averageCenter.y, depth);
    }
}
