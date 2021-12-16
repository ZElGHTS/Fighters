using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    [SerializeField] private CreateOrJoinRoomCanvas createOrJoinRoomCanvas;
    [SerializeField] private CurrentRoomCanvas currentRoomCanvas;

    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas
    {
        get => createOrJoinRoomCanvas;
        set => createOrJoinRoomCanvas = value;
    }
    
    public CurrentRoomCanvas CurrentRoomCanvas
    {
        get => currentRoomCanvas;
        set => currentRoomCanvas = value;
    }

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        CreateOrJoinRoomCanvas.FirstInitialize(this);
        CurrentRoomCanvas.FirstInitialize(this);
    }
}
