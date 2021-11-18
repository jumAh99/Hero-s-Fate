using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement; 

public class JoinGame : MonoBehaviourPunCallbacks{
    public override void OnJoinedRoom(){
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2){
            PhotonNetwork.LoadLevel("GameSceneMultiplayer"); 
        } 
    }
}
