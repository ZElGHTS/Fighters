using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestConnectScript : MonoBehaviourPunCallbacks
{
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to server");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName);
        if (!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();

        base.OnConnectedToMaster();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server because : " + cause);
        
        base.OnDisconnected(cause);
    }

    // public override void OnJoinedLobby()
    // {
    //     Debug.Log("Joined lobby!");
    //     base.OnJoinedLobby();
    // }

    private void Start()
    {
        Debug.Log("Connecting to server");
        //AuthenticationValues authValues = new AuthenticationValues("0");
        //PhotonNetwork.AuthValues = authValues;
        PhotonNetwork.SendRate = 20; // base is 20
        PhotonNetwork.SerializationRate = 10; // base is 10
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.Settings.NickName;
        PhotonNetwork.GameVersion = MasterManager.Settings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }
}
