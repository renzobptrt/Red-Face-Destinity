using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float currentSpeed = 1f;

    private Rigidbody2D rg2d;

    void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameManager.sharedInstance.gameState == GameState.inGame)
        {
            if (rg2d.velocity.x > -0.01f && rg2d.velocity.x < 0.01f)
            {
                currentSpeed = -currentSpeed;
            }
            Direccion();
            Run(currentSpeed);
        }

    }

    void Run(float velocity)
    {
        rg2d.velocity = new Vector2(velocity, rg2d.velocity.y);
    }

    void Direccion()
    {
        if (currentSpeed > 0.1f)
        {
            rg2d.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(currentSpeed < -0.1f)
        {
            rg2d.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        if(otherCollider.gameObject.tag == "Player"){
            float yOffSet = 0.9f;
            if(this.transform.position.y + yOffSet < otherCollider.transform.position.y){
                otherCollider.SendMessage("SaltoSobreEnemigo");
                Destroy(gameObject);
            }else{
                otherCollider.SendMessage("GolpeDeEnemigo",this.transform.position.x);
            }
        }
    }

}
