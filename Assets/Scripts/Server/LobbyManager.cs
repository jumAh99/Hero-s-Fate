using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using Photon.Realtime; 
using UnityEngine.UI; 
using TMPro; 

public class LobbyManager : MonoBehaviourPunCallbacks{
    public InputField roomInputField; 
    public GameObject lobbyPannel; 
    public GameObject roomPannel; 
    public Text roomName; 
    public RoomItemPrefab roomItem; 
    List<RoomItemPrefab> roomItemList = new List<RoomItemPrefab>();
    public Transform contentObj; 

    private void Start(){
        PhotonNetwork.JoinLobby(); 
    }

    public void OnClickCreate(){
        if(roomInputField.text.Length >= 1){
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions(){MaxPlayers = 2}); 
        }
    }

    public override void OnJoinedRoom(){
        lobbyPannel.SetActive(false); 
        roomPannel.SetActive(true); 
        roomName.text = "Current Room: " + PhotonNetwork.CurrentRoom.Name; 
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        UpdateRoomList(roomList); 
    }

    void UpdateRoomList(List<RoomInfo> info){
        foreach(RoomItemPrefab item in roomItemList){
            Destroy(item.gameObject); 
        }

        roomItemList.Clear(); 

        foreach(RoomInfo room in info){
            RoomItemPrefab newRoom = Instantiate(roomItem, contentObj); 
            newRoom.SetRoomName(room.Name);
            roomItemList.Add(newRoom); 
        }
    }
}
