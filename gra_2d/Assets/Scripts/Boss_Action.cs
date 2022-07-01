using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Action : MonoBehaviour
{
    bool canAttack = true, canBoulder = true;
    float range;
    Transform target;
    public Transform bossAttackHandler;
    public GameObject boulder;
    int j;

    void Start(){
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        range = Vector3.Distance(this.transform.position, target.position);
        if(range <= 3f) {
            if(canAttack) {
                Attack();
                canAttack = false;
            }
        }
            
    }

    void Attack(){
        if(canAttack == true) InvokeRepeating("SpawnBoulder", 0f, 1f);
        StartCoroutine(Attacking());
    }

    IEnumerator Attacking(){
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
    }
    IEnumerator Boulder(){
        yield return new WaitForSeconds(0.5f);
        canBoulder = true;
    }
    void SpawnBoulder(){
        float offX = Random.Range(-3.0f, 3.0f);
        float offY = Random.Range(-3.0f, 3.0f);
        var position = new Vector3(this.gameObject.transform.position.x + offX, this.gameObject.transform.position.y + offY, 0f);
        Instantiate(boulder, position, Quaternion.identity, bossAttackHandler);
    }
}
