using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SelectManager : MonoBehaviour
{
    public void OnSelect()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != 2)
        {
            PhotonNetwork.LoadLevel("WaitingScene");
        }
        else
        {
            PhotonNetwork.LoadLevel("GameSceneMultiplayer");
        }
    }
}
