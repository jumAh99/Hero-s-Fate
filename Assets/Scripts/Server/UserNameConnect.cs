using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using UnityEngine.UI; 
using TMPro; 
using UnityEngine.SceneManagement; 
public class UserNameConnect : MonoBehaviourPunCallbacks{
    public TextMeshProUGUI usernameInput; 
    public TextMeshProUGUI buttonText; 

    public void OnClickConnect(){
        if(usernameInput.text.Length >= 1){
            PhotonNetwork.NickName = usernameInput.text; 
            buttonText.text = "CONNECTING . . . "; 
            PhotonNetwork.ConnectUsingSettings(); 
        }
    }

    public override void OnConnectedToMaster(){
        SceneManager.LoadScene("Lobby");
    }
}
