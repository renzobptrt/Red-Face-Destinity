using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager sharedInstance;
    
    public AudioSource audio;

    void Awake(){
        sharedInstance = this;
    }
    void Start(){

    }

    void Update(){

    }

    public void ChangeBGM(AudioClip music){
        audio.Stop();
        audio.clip = music;
        audio.Play();
    }
    
}
