using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField] private PlayerListingsMenu playerListingsMenu;
    [SerializeField] private LeaveRoomMenu leaveRoomMenu;

    public LeaveRoomMenu LeaveRoomMenu
    {
        get => leaveRoomMenu;
        set => leaveRoomMenu = value;
    }
    private RoomsCanvases _roomsCanvases;
    
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
        playerListingsMenu.FirstInitialize(canvases);
        leaveRoomMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
