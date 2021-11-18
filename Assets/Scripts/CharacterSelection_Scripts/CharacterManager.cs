using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this is for TextMeshPro assets
using TMPro;
//use the SceneManagement asset to control scenes 
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    //reference to character database 
    public Character_Database charDb;

    //SELECT OPTION ON THE LIST 
    private int selectedCharacter;

    //assigning each object on the UI to a variable 
    public TextMeshProUGUI textName; //name of the character on Unity
    public SpriteRenderer renderObj; //sprite of the character on Unity

    // Start is called before the first frame update
    void Start()
    {
        //check is there is any saved data look Save() for more info 
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
        //when the user goes to the menu the first option will be the first character 
        UpdateCharacter(selectedCharacter);
    }

    //when next button is pressed
    public void PressNext()
    {
        //increase the selected option value by 1
        selectedCharacter++;

        //check weather the index has reached the max number of characters
        if (selectedCharacter >= charDb.charactersCount)
        {
            //set the val to 0 so you can recyle through the characaters 
            selectedCharacter = 0;
        }

        //update by each press
        UpdateCharacter(selectedCharacter);

        //save the player choice
        Save();
    }

    //when back button is pressed
    public void PressBack()
    {
        //instead of goign up the option value will go down 
        selectedCharacter--;

        //if the user press back on the first character 
        if (selectedCharacter < 0)
        {
            selectedCharacter = charDb.charactersCount - 1;
        }

        //update character info 
        UpdateCharacter(selectedCharacter);

        //save the player choice 
        Save();
    }

    //update character name 
    public void UpdateCharacter(int selectedCharacter)
    {
        //get the information of the current character
        CharacterInfo character = charDb.getCharacter(selectedCharacter);
        //set the right sprite to the character
        renderObj.sprite = character.GAME_OBJ;
        //create a character obj and add the name to it 
        textName.SetText(character.CHARACTER_NAME);
    }

    //load the saved data
    private void Load()
    {
        //load the option that was saved on the selectedCharacter key name
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
    }

    //save the data on key
    private void Save()
    {
        //save the selectedCharacter variable as a key name 
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
    }

    //function to change to GameScene
    public void LoadScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
