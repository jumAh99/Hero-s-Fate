using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//allow unity to add objects trough the assset menu
[CreateAssetMenu]
public class SelectedChamp : ScriptableObject{
    public Champ[] charList; 

    //get the number of characters in the game
    public int charactersCount{
        get{
            return charList.Length; 
        }
    }
    public Champ getCharacter(int index){
        return charList[index]; 
    }
}
