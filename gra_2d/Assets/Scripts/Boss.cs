using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static float maxHP = 200f, attackPower = 30f, armor = 5f, movement_speed = 2f;
    public float currentHP;

    public static Animator anim;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        currentHP = maxHP;
    }

    void Update()
    {        
        if(currentHP <= 0f) {
            killEnemy();
            FinishGameMenuHandler.showResult(true);
            }   
    }

    void killEnemy(){
        Debug.Log("Enemy "+this.name+" died!");
        Destroy(this.gameObject);
        GameMenuHandler.winWindow();
    }

    public void takeDamage(float damage){
        anim.SetTrigger("Hit");
        if (damage-armor > 0f) currentHP = currentHP-(damage-armor);
        else currentHP = currentHP - 1f;
    }
}
