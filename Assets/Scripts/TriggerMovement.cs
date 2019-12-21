using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    public bool movimientoAdelante;
    public int contador = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {   
        if(col.tag == "Player"){
            Debug.Log("Choque contra el jugador");
            return;
        }

        //Debug.Log("Girar enemigo");
        if (movimientoAdelante == true)
        {
            Enemy.voltearEnemigo = true;
        }
        else
        {
            Enemy.voltearEnemigo = false;
        }
        movimientoAdelante = !movimientoAdelante;
    }
}
