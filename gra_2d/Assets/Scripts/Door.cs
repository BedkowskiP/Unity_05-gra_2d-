using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if(Player_Action.haveKey){
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        canvas.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        canvas.SetActive(false);
    }
}
