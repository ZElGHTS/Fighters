using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkedPrefab
{
    public GameObject Prefab;
    public string Path;

    public NetworkedPrefab(GameObject obj, string path)
    {
        Prefab = obj;
        Path = ReturnPrefabPathModified(path);
        //Assets/Resources/File.prefab
        //Resources/File.prefab
    }

    private string ReturnPrefabPathModified(string path)
    {
        var extensionLength = System.IO.Path.GetExtension(path).Length;
        var additionalLength = 10;
        var startIndex = path.ToLower().IndexOf("resources", StringComparison.Ordinal);

        if (startIndex == -1) return string.Empty;

        return path.Substring(startIndex + additionalLength, path.Length - (additionalLength + startIndex + extensionLength));
    }
}
