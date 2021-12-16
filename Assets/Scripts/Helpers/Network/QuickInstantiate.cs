using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuickInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private static GameObject player;

    public static GameObject Prefab
    {
        get => player;
    }

    private void Awake()
    {
        var offset = Random.insideUnitCircle * 3f; // replace by spawn point instead of random
        var position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z);
        
        player = MasterManager.NetworkInstantiate(prefab, position, Quaternion.identity);
    }
}
