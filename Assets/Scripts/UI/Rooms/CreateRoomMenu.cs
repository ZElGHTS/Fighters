using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text roomName;
    
    private RoomsCanvases _roomsCanvases;
    
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    public void OnClick_CreateRoom()
    {
        //Create
        //JoinOrCreate
        if (!PhotonNetwork.IsConnected) return;
        
        var options = new RoomOptions();
        options.BroadcastPropsChangeToAll = true;
        //options.PublishUserId = true;
        options.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Successfully!");
        _roomsCanvases.CurrentRoomCanvas.Show();
        
        base.OnCreatedRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message);
        
        base.OnCreateRoomFailed(returnCode, message);
    }
}
