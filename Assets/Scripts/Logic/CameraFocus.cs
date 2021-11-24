using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] public float halfBoundsX = 20f;
    [SerializeField] public float halfBoundsY = 15f;
    [SerializeField] public float halfBoundsZ = 15f;
    [SerializeField] public Bounds focusBounds;
    
    private void Update()
    {
        var position = gameObject.transform.position;
        var bounds = new Bounds();
        
        bounds.Encapsulate(new Vector3(position.x - halfBoundsX, position.y - halfBoundsY, position.z - halfBoundsZ));
        bounds.Encapsulate(new Vector3(position.x + halfBoundsX, position.y + halfBoundsY, position.z + halfBoundsZ));

        focusBounds = bounds;
    }
}
