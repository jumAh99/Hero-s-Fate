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

    //start the battle 
    IEnumerator MultiplayerSetupBattle(){
        //playerOne position 
        double posOneX = -967.45; 
        double posOneY = -544.37;

        //playerTwo position 
        double posTwoX = -952.6; 
        double posTwoY = -544.31;

        //spawn the player ONE to the spawn point
        Vector2 posPlayerOne = new Vector2((float)posOneX, (float)posOneY); 
        Champ champOneAsset = dbChamp.getCharacter(selectedCharacter);
        GameObject playerOneObj = PhotonNetwork.Instantiate(champOneAsset.nameChamp, posPlayerOne, Quaternion.identity); // no rotation 
        playerUnitOne = playerOneObj.GetComponent<Unit>(); 

        dialogueText.text = playerUnitOne.unitName + " has appeared. . .";

        //populate the HUB with player information
        playerOneHUB.setHUB(playerUnitOne);

        yield return new WaitForSeconds(1f);

        //spawn the player TWO to the spawn point
        Vector2 posPlayerTwo = new Vector2((float)posTwoX, (float)posTwoY); 
        Champ champTwoAsset = dbChamp.getCharacter(selectedCharacter);
        GameObject playerTwoObj = PhotonNetwork.Instantiate(champTwoAsset.nameChamp, posPlayerTwo, Quaternion.identity); 
        playerUnitTwo = playerTwoObj.GetComponent<Unit>();  

        dialogueText.text = playerUnitOne.unitName + " has appeared. . .";

        //populate teh HUB with player information
        playerTwoHUB.setHUB(playerUnitTwo);

        yield return new WaitForSeconds(2f);

        //player ONE turn
        state = MultiplayerBattleState.playerONEturn; 
        PlayerOneTurn();
    }

    //let the player know whos turn it is
    void PlayerOneTurn(){
        dialogueText.text = "Player One turn."; 
    }
    //let the player know whos turn it is
    void PlayerTwoTurn(){
        dialogueText.text = "Player Two turn"; 
    }
    //attack function for player ONE 
    IEnumerator PlayerOneAttack(){
        //basic damage to enemy
        bool isDead = playerUnitTwo.TakeDamage(playerUnitOne.damage); 
        //update the current hp of enemy
        playerTwoHUB.setHP(playerUnitTwo.currentHP);
        //update the dialogue text 
        dialogueText.text = "Player 1 Attack Successful"; 

        //wait for 2 seconds
        yield return new WaitForSeconds(2f); 

        //check if enemy is dead
        if(isDead){
            //end battle
            state = MultiplayerBattleState.WON; 
            EndBattle(); 
        }else{
            //enemy turn
            state = MultiplayerBattleState.playerTWOturn;
            PlayerTwoTurn();
        }
    }

    //attack function for player TWO 
    IEnumerator PlayerTwoAttack(){
        //basic damage to enemy
        bool isDead = playerUnitOne.TakeDamage(playerUnitTwo.damage); 
        //update the current hp of enemy
        playerOneHUB.setHP(playerUnitOne.currentHP);
        //update the dialogue text 
        dialogueText.text = "Player 2 Attack Successful"; 

        //wait for 2 seconds
        yield return new WaitForSeconds(2f); 

        //check if enemy is dead
        if(isDead){
            //end battle
            state = MultiplayerBattleState.WON; 
            EndBattle(); 
        }else{
            //enemy turn
            state = MultiplayerBattleState.playerONEturn;
            PlayerOneTurn();
        }
    }

    //ATTACK BUTTON FOR THE PLAYER ONE
    public void AttackButtonONE(){
        if(state != MultiplayerBattleState.playerONEturn){
            return; 
        }else{
            StartCoroutine(PlayerOneAttack()); 
        }
    }

    //ATTACK BUTTON FOR THE PLAYER TWO
    public void AttackButtonTWO(){
        if(state != MultiplayerBattleState.playerTWOturn){
            return; 
        }else{
            StartCoroutine(PlayerTwoAttack()); 
        }
    }

    void EndBattle(){
        if(state == MultiplayerBattleState.WON){
            dialogueText.text = "Enemy Defeated"; 
        }else if(state == MultiplayerBattleState.LOST){
            dialogueText.text = "You Lost"; 
        }
    }

    //load the saved data
    private void Load(){
        //load the option that was saved on the selectedCharacter key name
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter"); 
    }
}
