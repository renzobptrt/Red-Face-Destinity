using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{
    int contador = 0;
    void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.gameObject.tag == "Player"){
            contador++;
        }
        if (contador == 1)
        {
            LevelGenerator.sharedInstance.AddLevelBlock();
            LevelGenerator.sharedInstance.RemoveOldestLevelBlock();
        }
        contador=0;
    }
}
