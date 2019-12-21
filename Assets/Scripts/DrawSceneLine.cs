using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSceneLine : MonoBehaviour
{
    
    public Transform origen;
    public Transform destino;
    
    void OnDrawGizmosSelected(){
        if(origen!=null && destino!=null){
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(origen.position, destino.position);
            Gizmos.DrawSphere(origen.position,0.15f);
            Gizmos.DrawSphere(destino.position,0.15f);
        }
    }

}
