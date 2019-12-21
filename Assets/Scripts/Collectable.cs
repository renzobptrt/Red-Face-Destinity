using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum collectableType{
    healthPotion,
    manaPotion,
    money
}

public class Collectable : MonoBehaviour
{   
    //Variable del enumerator
    public collectableType type = collectableType.money;
    //Variable que tendre el sonido de la moneda al momento de coleccionarla
    public AudioClip collectableSound;
    private AudioSource audio;


    //Variable para saber si el objeto fue recogido o no 
    //bool isCollected = false;

    //Valor del objeto
    public int value = 0;

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            Collect();
        }
    }

    //Metodo para mostrar el objeto
    void Show(){
        //Activamos la imagen de la moneda y la animacion
        this.GetComponent<SpriteRenderer>().enabled = true;
        //Activamos el collider
        if(this.type != collectableType.money){
            this.GetComponent<CircleCollider2D>().enabled = true;
        }else{
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        //isCollected = false;
    }

    //Metodo para ocultar el objeto
    void Hide(){
        //Desactivamos la imagen de la moneda y la animacion
        this.GetComponent<SpriteRenderer>().enabled = false;
        //Desactivamos el collider
        if(this.type != collectableType.money){
            this.GetComponent<CircleCollider2D>().enabled = false;
        }else{
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        //isCollected = true;
    }

    //Metodo para colleccionar
    void Collect(){
        //isCollected= true;
        audio = GetComponent<AudioSource>();
        Hide();
        if(collectableSound!=null && audio!=null){
            GetComponent<AudioSource>().PlayOneShot(this.collectableSound);
        }

        switch(this.type){
            case collectableType.money:
                GameManager.sharedInstance.CollectCoins(value);
                break;
            case collectableType.healthPotion:
                PlayerContoller.sharedInstance.CollectHealth(value);
                break;
            case collectableType.manaPotion:
                PlayerContoller.sharedInstance.CollectMana(value);
                break;
        }
    }
}
