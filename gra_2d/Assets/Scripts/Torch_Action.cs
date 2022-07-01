using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_Action : MonoBehaviour
{
    public GameObject fire_particle, text;
    bool isFireOn = false;
    private void OnTriggerEnter2D(Collider2D other){
        if(!isFireOn) text.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(text.activeInHierarchy) text.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(Player_Action.isTorch) 
        if(Input.GetKey(KeyCode.E) && !isFireOn){
            isFireOn = true;
            fire_particle.SetActive(true);
            StartCoroutine(FireLength());
        }
    }

    IEnumerator FireLength(){
        yield return new WaitForSeconds(30f);
        fire_particle.SetActive(false);
        isFireOn = false;
    }

}
