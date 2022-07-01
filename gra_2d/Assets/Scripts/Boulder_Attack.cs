using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder_Attack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    // Start is called before the first frame update
    void Attack(){
        Collider2D[] Players = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D player in Players){
            Debug.Log("Player "+player.name+" got hit!");
            player.GetComponent<Player_Controller>().takeDamage(Boss.attackPower);
        }
        Destroy(this.gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 0.1f);
    }
}
