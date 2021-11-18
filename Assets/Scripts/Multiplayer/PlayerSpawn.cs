using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawn : MonoBehaviour
{
    public SelectedChamp champ;
    int selectedCharacter;
    public BattleHUB playerHUBOne;
    public BattleHUB playerHUBTwo;
    public Transform spawn_player_one;
    public Transform spawn_player_two;
    Unit playerUnitOne;
    Unit playerUnitTwo;

    private void Start()
    {

        if (!PlayerPrefs.HasKey("selectedCharacter"))
        {
            //if there is no option then set the value of selectedCharacter to 0
            selectedCharacter = 0;
        }
        else
        {
            //call the load function to cycle through the characters check Load()
            Load();
        }

        //update player one
        UpdatePlayer(selectedCharacter, spawn_player_one, playerUnitOne, playerHUBOne);
        //update player two
        UpdatePlayer(selectedCharacter, spawn_player_two, playerUnitTwo, playerHUBTwo);
    }

    public void UpdatePlayer(int selectedCharacter, Transform pos, Unit unit, BattleHUB hub)
    {
        Champ champAsset = champ.getCharacter(selectedCharacter);
        GameObject result = (GameObject)PhotonNetwork.Instantiate(champAsset.nameChamp, pos.position, Quaternion.identity);
        unit = result.GetComponent<Unit>();

        //update player hub 
        hub.setHUB(unit);
    }
    //load the saved data
    private void Load()
    {
        //load the option that was saved on the selectedCharacter key name
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
    }
}
