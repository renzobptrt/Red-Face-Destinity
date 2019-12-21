using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType{
    healthBar,
    manaBar
}

public class PlayerBar : MonoBehaviour
{
    private Slider slider;
    
    public BarType type;
    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        switch(this.type){
            case BarType.healthBar:
                slider.value = PlayerContoller.sharedInstance.GetHealthPoints();
                break;
            case BarType.manaBar:
                slider.value = PlayerContoller.sharedInstance.GetManaPoints();
                break;
        }
    }
}
