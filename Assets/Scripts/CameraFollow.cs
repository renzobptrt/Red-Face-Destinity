using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow sharedInstance;

    public Transform target;

    /* Dejar una pequeña distancia de seguimiento */
    public Vector3 offset = new Vector3(0.1f,0f,-10f);

    /*  Tiempo donde la camara se quedara quita y luego 
        comenzara a seguir
    */
    public float dampTime = 0.3f;

    /* Velocidad de la carama */
    public Vector3 velocity = Vector3.zero;

    void Awake(){
        /* Para intentar que la actualizacion de 
            frames sea constante al numero que 
            le indique si es que puede.*/
        Application.targetFrameRate = 60;
        sharedInstance = this;
    }

    public void ResetCameraPosition(){
        Vector3 destination ;
        destination = new Vector3(target.position.x, offset.y, offset.z);
        
        this.transform.position = destination;
    }   

    void Update(){
        Vector3 posicionInicial = PlayerContoller.sharedInstance.getStartPosition();
        /* Nota: Fondos siempre se configuran en la cámara */
        /*if(target.position.x <= posicionInicial.x){
            
        }*/
        Vector3 destination ;
        Vector3 destinationInicial;
        destination = new Vector3(target.position.x, offset.y, offset.z);
        destinationInicial = new Vector3(posicionInicial.x,offset.y,offset.z);
        /* Mueve la camara de forma suave */
        if(destination.x <= posicionInicial.x-2){
            this.transform.position = Vector3.SmoothDamp(this.transform.position,destinationInicial, ref velocity, dampTime);
        }else{
            this.transform.position = Vector3.SmoothDamp(this.transform.position,destination, ref velocity, dampTime);
        }

        
    }
}
