using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public GameObject player_spriter;
    public Animator animator;
    public static Rigidbody2D rig2d;
    private Vector3 moveDir;

    private float bonus_movement_speed = 0f;

    private bool facingRight = true, isRotating = false;

    void Start()
    {
        rig2d = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate() 
    {
        rig2d.velocity = moveDir * (Player_Attributes.movement_speed + bonus_movement_speed);
    }

    void Flip(){
        isRotating = true;

        StartCoroutine(Rotate180());

        facingRight = !facingRight; 
    }

    void Movement(){
        float x = 0f;
        float y = 0f;

        if(Input.GetKey(KeyCode.W)){
            y = +1f;
        }
        if(Input.GetKey(KeyCode.A)){
            x = -1f;
        }
        if(Input.GetKey(KeyCode.S)){
            y = -1f;
        }
        if(Input.GetKey(KeyCode.D)){
            x = +1f;
        }

        moveDir = new Vector3(x, y).normalized;
        if(x > 0 && !facingRight && !isRotating){
            Flip();
        }
        if(x < 0 && facingRight  && !isRotating){
            Flip();
        }

        if(x != 0 || y != 0) animator.SetBool("Run", true);
        else animator.SetBool("Run", false);
    }

    IEnumerator Rotate180()
    {
        float amountRotated = 0f;
        while (amountRotated < 180f) {
            float frameRotation = 600 * Time.deltaTime;
            player_spriter.transform.Rotate(0, frameRotation, 0);
            amountRotated += frameRotation;
            yield return new WaitForEndOfFrame();
        }
        if(player_spriter.transform.rotation.y > 0) player_spriter.transform.eulerAngles = new Vector3(0, 180, 0);
        if(player_spriter.transform.rotation.y < 0) player_spriter.transform.eulerAngles = new Vector3(0, 0, 0);
        isRotating = false;
    }
    public void takeDamage(float damage){
        animator.SetTrigger("Hit");
        if (damage-Player_Attributes.armor > 0f) Player_Attributes.currentHP = Player_Attributes.currentHP-(damage-Player_Attributes.armor);
        else Player_Attributes.currentHP = Player_Attributes.currentHP - 1f;
    }
}
