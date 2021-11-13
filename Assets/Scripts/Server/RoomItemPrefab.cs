using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class RoomItemPrefab : MonoBehaviour{
    public Text roomName; 

    public void SetRoomName(string _name){
        roomName.text = _name; 
    }
}
