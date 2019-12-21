using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Jugador entro en zona de muerte");
            //PlayerContoller.sharedInstance.KillPlayer();
            PlayerContoller.sharedInstance.Dead();
        }
    }
}
