using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void NewGame(){
        SceneManager.LoadScene("Game");
        SceneManager.UnloadSceneAsync("Menu");
    }

    public void Quit(){
        Application.Quit();
    }
}
