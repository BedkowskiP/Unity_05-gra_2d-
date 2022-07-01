using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : MonoBehaviour
{
    public static Animator anim;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    bool canPlayerAttack = true;
    public static bool isTorch = false;
    public GameObject weapon, torch, torchLight;
    public static bool haveKey = false;

    void Start(){
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1)){
            canPlayerAttack = false;
            isTorch = true;
            weapon.SetActive(false);
            torchLight.SetActive(true);
            torch.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.Mouse1)) {
            canPlayerAttack = true;
            isTorch = false;
            weapon.SetActive(true);
            torchLight.SetActive(false);
            torch.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && canPlayerAttack)
        {
            anim.SetTrigger("Attack");
            canPlayerAttack = false;
            StartCoroutine(Attacking());
        }

        
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("Enemy "+enemy.name+" got hit!");
            try{
                enemy.GetComponent<Enemy>().takeDamage(Player_Attributes.attackPower);
            } catch{
                enemy.GetComponent<Boss>().takeDamage(Player_Attributes.attackPower);
            }
        }

    }

    void OnDrawGizmosSelected() {
        if(attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator Attacking(){
        yield return new WaitForSeconds(1f);
        canPlayerAttack = true;
    }

    public static void keyStatus(){
        haveKey = !haveKey;
    }


}
