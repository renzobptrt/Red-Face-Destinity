using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{   

    private Rigidbody2D rb2d;

    void Start(){
        //PlayerContoller.sharedInstance = GetComponentInParent<PlayerContoller>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        PlayerContoller.sharedInstance.isGrounded = true;
    }


    void OnCollisionEnter2d(Collision2D collider){
        if(collider.gameObject.tag == "Platform"){
            //PlayerContoller.sharedInstance.Run(0.0f);
            PlayerContoller.sharedInstance.transform.parent = collider.transform;
            
            PlayerContoller.sharedInstance.isGrounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D collider){
        if(collider.gameObject.tag == "Ground"){ 
            PlayerContoller.sharedInstance.isGrounded = true;
        }

        if(collider.gameObject.tag == "Platform"){ 
            //PlayerContoller.sharedInstance.Run(0.0f);
            PlayerContoller.sharedInstance.transform.parent = collider.transform;
            
            PlayerContoller.sharedInstance.isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D collider){
        if(collider.gameObject.tag == "Ground"){
            PlayerContoller.sharedInstance.isGrounded = false;
        }
        
        if(collider.gameObject.tag == "Platform"){
            PlayerContoller.sharedInstance.transform.parent = null;
            PlayerContoller.sharedInstance.isGrounded = false;
        }
    }
}
