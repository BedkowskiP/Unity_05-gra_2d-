using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static float maxHP = 100f, attackPower = 20f, armor = 5f, movement_speed = 3f;
    public float currentHP;

    public static Animator anim;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        currentHP = maxHP;
    }

    void Update()
    {        
        if(currentHP <= 0f) killEnemy();        
    }

    void killEnemy(){
        Debug.Log("Enemy "+this.name+" died!");
        Destroy(this.gameObject);
    }

    public void takeDamage(float damage){
        anim.SetTrigger("Hit");
        if (damage-armor > 0f) currentHP = currentHP-(damage-armor);
        else currentHP = currentHP - 1f;
    }
}
