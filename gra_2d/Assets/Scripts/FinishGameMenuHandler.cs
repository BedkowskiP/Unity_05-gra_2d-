using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameMenuHandler : MonoBehaviour
{
    public static GameObject GameFinishMenuS, winTxtS, loseTxtS, gameHandlerS;
    public GameObject GameFinishMenu, winTxt, loseTxt, gameHandler;
    private void Awake() {
        GameFinishMenuS = GameFinishMenu;
        gameHandlerS = gameHandler;
        winTxtS = winTxt;
        loseTxtS = loseTxt;
    }

    public static void showResult(bool win){
        GameFinishMenuS.SetActive(true);
        if(win){
            winTxtS.SetActive(true);
            loseTxtS.SetActive(false);
        } else {
            winTxtS.SetActive(false);
            loseTxtS.SetActive(true);
        }
        gameHandlerS.SetActive(false);
    }
}
