using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuHandler : MonoBehaviour
{
    public GameObject gameHandler, gameMenu;
    bool pause = false;
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && !pause){
            pause = true;
            gameHandler.SetActive(false);
            gameMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pause){
            returnToGame();
        }
    }

    public void returnToGame(){
        Debug.Log("Click");
        pause = false;
        gameHandler.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void exitToMainMenu(){
        SceneManager.LoadScene("Menu");
        SceneManager.UnloadSceneAsync("Game");
    }

    public static void winWindow(){

    }
    public static void gameOverWindow(){

    }
}
