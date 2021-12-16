using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{
    [SerializeField] private GameSettings settings;

    [SerializeField] private List<NetworkedPrefab> networkedPrefabs = new List<NetworkedPrefab>();
    public static GameSettings Settings
    {
        get => Instance.settings;
        set => Instance.settings = value;
    }

    public static GameObject NetworkInstantiate(GameObject obj, Vector3 position, Quaternion rotation)
    {
        foreach (var networkedPrefab in Instance.networkedPrefabs)
        {
            if (networkedPrefab.Prefab == obj)
            {
                if (networkedPrefab.Path != string.Empty)
                {
                    return PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                }
                Debug.LogError("Path is empty for gameobject : " + networkedPrefab.Prefab);
                return null;
            }
        }
        return null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PopulateNetworkedPrefabs()
    {
#if UNITY_EDITOR
        Instance.networkedPrefabs.Clear();

        var results = Resources.LoadAll<GameObject>("");
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].GetComponent<PhotonView>() != null)
            {
                var path = AssetDatabase.GetAssetPath(results[i]);
                Instance.networkedPrefabs.Add(new NetworkedPrefab(results[i], path));
            }
        }
#endif
    }
}
