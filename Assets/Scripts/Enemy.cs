using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 4f;
    private Rigidbody2D rg2d;

    private Vector3 startPosition;

    public static bool voltearEnemigo;

    void Awake()
    {
        rg2d = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        float currentSpeed;

        if (voltearEnemigo == true)
        {
            //Aqui la velocidad es positiva
            currentSpeed = -runningSpeed;
            rg2d.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            //Aqui la velocidad es negativa
            currentSpeed = runningSpeed;
            rg2d.transform.localScale = new Vector3(-1, 1, 1);
        }

        if (GameManager.sharedInstance.gameState == GameState.inGame)
        {
            Run(currentSpeed);
        }
    }

    void Run(float velocity)
    {
        rg2d.velocity = new Vector2(velocity, rg2d.velocity.y);
    }
}
