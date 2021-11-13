using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LoseWinManager : MonoBehaviour{
    public static void OnTryAgain(){
        SceneManager.LoadScene("CharSelect");
    }
    public static void OnMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
