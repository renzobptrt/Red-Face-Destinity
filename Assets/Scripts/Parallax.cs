using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{      
    public float speed = 0.0f;

    private Rigidbody2D rgbd;
    // Start is called before the first frame update
    void Awake()
    {
        rgbd = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.rgbd.velocity = new Vector2(speed,0);

        float posicionPadre = this.transform.parent.transform.position.x;

        if(this.transform.position.x - posicionPadre <= -19f){
            this.transform.position = new Vector3(posicionPadre + 19f, 
                                                  this.transform.position.y, 
                                                  this.transform.position.z);
        }
    }
}
