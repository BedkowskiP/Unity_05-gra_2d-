using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Attributes : MonoBehaviour
{
    public static float maxHP = 100f, attackPower = 60f, armor = 8f, movement_speed = 5f;
    public static float currentHP;

    public HPHandler hpHandler;
    
    public void Start()
    {
        hpHandler.setMaxHP(maxHP);
        currentHP = maxHP;
    }

    public void Update(){
        try{
            hpHandler.setHP(currentHP);
        } catch{

        }
        if(currentHP <= 0f) {
            FinishGameMenuHandler.showResult(false);
        } 
    }

    public static void pickUpPotion(){
        currentHP += 20f;
        if(currentHP > maxHP) currentHP = maxHP;
    }
    
}
