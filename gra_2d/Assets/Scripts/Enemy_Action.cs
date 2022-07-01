using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Action : MonoBehaviour
{
    public Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    bool canAttack = true;
    float range;
    Transform target;

    void Start(){
        anim = this.GetComponent<Animator>();
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        range = Vector3.Distance(this.transform.position, target.position);
        if(range <= 1f) 
            if(canAttack) {
                anim.SetTrigger("Attack");
                canAttack = false;
                StartCoroutine(Attacking());
            }
    }

    void Attack(){
        Collider2D[] Players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D player in Players){
            Debug.Log("Player "+player.name+" got hit!");
            player.GetComponent<Player_Controller>().takeDamage(Enemy.attackPower);
        }
    }

    IEnumerator Attacking(){
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
}
