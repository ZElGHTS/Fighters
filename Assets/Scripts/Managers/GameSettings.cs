using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private string gameVersion = "0.0.0";
    [SerializeField] private string nickName = "Fighter";
    
    public string GameVersion
    {
        get => gameVersion;
        set => gameVersion = value;
    }
    
    public string NickName
    {
        get => nickName + Random.Range(0, 999);
        set => nickName = value;
    }
}
