using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 

public class PlayerSpawn : MonoBehaviour{
    public GameObject[] arrPrefabChamp;
    public Transform[] spawnPointsList; 

    private void Start(){
        int randomNumber = Random.Range(0, spawnPointsList.Length); 
        //correct spawn point
        Transform spawnPoint = spawnPointsList[randomNumber]; 
        //correct player prefab
        GameObject playerToSpawn = arrPrefabChamp[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]]; 
        PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity); 
    } 
}
