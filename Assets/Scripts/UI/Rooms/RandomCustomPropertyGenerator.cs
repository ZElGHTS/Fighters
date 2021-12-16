using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField] private Text text;
    
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    public void OnClick_Button()
    {
        SetCustomNumber();
    }

    private void SetCustomNumber()
    {
        System.Random random = new System.Random();
        var result = random.Next(0, 99);

        text.text = result.ToString();

        _myCustomProperties["RandomNumber"] = result;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        //_myCustomProperties.Remove("RandomNumber");
        //PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
    }
}
