using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState
{
    //these are the different game state that can be possible in our game
    START, PLAYERTURN, WON, LOST, ENEMYTURN
}

public class BattleSystem : MonoBehaviour
{
    //to render in the game
    public SelectedChamp champ;
    public SelectedChamp champEnemy;
    public BattleState state;
    public Transform battle_station_player;
    public Transform battle_station_enemy;
    public TextMeshProUGUI dialogueText;
    //SELECT OPTION ON THE LIST 
    private int selectedCharacter;
    private int selectedEnemy;
    public BattleHUB playerHUB;
    public BattleHUB enemyHUB;
    Unit playerUnit;
    Unit enemyUnit;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;

        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
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

        System.Random ran = new System.Random();
        selectedEnemy = ran.Next(0, 4);


        //player spawn
        UpdateCharacter(selectedCharacter);
        //enemy spawn
        UpdateEnemy(selectedEnemy);


        dialogueText.text = enemyUnit.unitName + " has appeared. . .";

        playerHUB.setHUB(playerUnit);
        enemyHUB.setHUB(enemyUnit);

        yield return new WaitForSeconds(2f);
        //player turn
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    //update character  
    public void UpdateCharacter(int selectedCharacter)
    {
        Champ champAsset = champ.getCharacter(selectedCharacter);
        GameObject playerObj = Instantiate(champAsset.prefabChamp, battle_station_player);
        playerUnit = playerObj.GetComponent<Unit>();
    }

    //update enemy  
    public void UpdateEnemy(int selectedEnemy)
    {
        Champ champAsset = champEnemy.getCharacter(selectedEnemy);
        GameObject playerObj = Instantiate(champAsset.prefabChamp, battle_station_enemy);
        enemyUnit = playerObj.GetComponent<Unit>();
    }

    //load the saved data
    private void Load()
    {
        //load the option that was saved on the selectedCharacter key name
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            SceneManager.LoadScene("WinScene");
        }
        else if (state == BattleState.LOST)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUB.setHP(playerUnit.currentHP);

        dialogueText.text = "Attack Successful: " + " You take " + enemyUnit.damage;

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    //attack funtion for player basic attack 
    IEnumerator PlayerAttack()
    {

        if (playerUnit.currentAp >= 30)
        {
            //basic damage to enemy
            bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
            playerUnit.APReduct(100);

            //update the current hp of enemy
            playerHUB.setHP(playerUnit.currentHP);
            playerHUB.setAP(playerUnit.currentAp);
            enemyHUB.setHP(enemyUnit.currentHP);
            //update the dialogue text 
            dialogueText.text = "Attack Successful: " + "You deal " + playerUnit.damage;
            yield return new WaitForSeconds(1f);
            //check if enemy is dead
            if (isDead)
            {
                //end battle
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                //enemy turn
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "You dont have enough AP for this action. .";
            //enemy turn
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    //action when heal button is pressed
    IEnumerator PlayerApRegen()
    {
        playerUnit.APRegen(100);

        playerHUB.setAP(playerUnit.currentAp);
        dialogueText.text = "AP replenished by 100";

        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    //action when heal button is pressed
    IEnumerator PlayerRegen()
    {
        if (playerUnit.currentAp >= 150)
        {
            //set regen and ap cost
            playerUnit.Heal(playerUnit.regen);
            playerUnit.APReduct(150);
            //set the hub information
            playerHUB.setAP(playerUnit.currentAp);
            playerHUB.setHP(playerUnit.currentHP);
            //inform the user about the change
            dialogueText.text = "You have healed by : " + playerUnit.regen;

            yield return new WaitForSeconds(1f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogueText.text = "You dont have enough AP for this action. .";
            yield return new WaitForSeconds(1f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Your Turn: ";
    }

    //when attack button is pressed
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerAttack());
        }
    }

    //when fpRegen button is pressed
    public void OnAPRegen()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerApRegen());
        }
    }

    //when heal button is pressed
    public void OnHeal()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else
        {
            StartCoroutine(PlayerRegen());
        }
    }
}

