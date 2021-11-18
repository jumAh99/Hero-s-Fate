using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    //go to char select
    public void playGame(){
        SceneManager.LoadScene("CharSelect");
    }
    //go to multiplayer
    public void OnMultiplayer(){
        SceneManager.LoadScene("ConnectingScene");
    }
    //close application 
    public void quitGame(){
        Debug.Log("Quit");
        Application.Quit(); 
    }
}
