using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allow unity to add objects trough the assset menu
[CreateAssetMenu]
public class Character_Database : ScriptableObject{
    public CharacterInfo[] charList; 

    //get the number of characters in the game
    public int charactersCount{
        get{
            return charList.Length; 
        }
    }
    public CharacterInfo getCharacter(int index){
        return charList[index]; 
    }
}
