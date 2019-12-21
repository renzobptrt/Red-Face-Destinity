using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewInGame : MonoBehaviour
{   
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.gameState == GameState.inGame ||
           GameManager.sharedInstance.gameState == GameState.inGameOver){
            int coins = GameManager.sharedInstance.collectedCoin;
            this.coinsText.text = coins.ToString();
            
        }
        if(GameManager.sharedInstance.gameState == GameState.inGame){
            float distance = PlayerContoller.sharedInstance.GetDistance();
            this.scoreText.text = distance.ToString("f2");
            
            float maxScore = PlayerPrefs.GetFloat("maxScore",0);
            this.maxScoreText.text = maxScore.ToString("f2");
        }   
    }
}
