using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; 
using TMPro;

public enum MultiplayerBattleState {
    //these are the different game state that can be possible in our game
    START, playerONEturn, WON, LOST, playerTWOturn
} 

public class Multiplayer_BattleSystem : MonoBehaviour{
    //get the saelected cham from the character selection screen 
    public SelectedChamp dbChamp; 
    //create a multiplayer battle state variable 
    public MultiplayerBattleState state;

    //SELECT OPTION ON THE LIST 
    private int selectedCharacter;

    //SELECT OPTION ON THE LIST 
    private int selectedCharacterONE;
    private int selectedCharacterTWO;

    //set the data to teh player hubs 
    public BattleHUB playerOneHUB; 
    public BattleHUB playerTwoHUB; 

    //create a Unit object so we are able to assign the data to the prefabs 
    Unit playerUnitOne; 
    Unit playerUnitTwo; 

    //dialogue 
    public TextMeshProUGUI dialogueText;

    
}
