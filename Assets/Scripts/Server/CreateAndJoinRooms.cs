using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun; 

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks{
    //create variables for the two input fields 
    public InputField createInput; 
    public InputField joinInput;
    
    //function for the create button
    public void OnCreate(){
        PhotonNetwork.CreateRoom(createInput.text); 
    }
    //function for join button
    public void OnJoin(){
        PhotonNetwork.JoinRoom(joinInput.text); 
    }
    //make the user join the game scene 
    public override void OnJoinedRoom(){
          PhotonNetwork.LoadLevel("CharSelectMultiplayer"); 
    }
}
