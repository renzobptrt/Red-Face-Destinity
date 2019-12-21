using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Posibles estados del videojuego
public enum GameState
{
    inGame,
    inGameOver,
    inMenu
}

public class GameManager : MonoBehaviour
{

    public static GameManager sharedInstance;

    //Variable para saber en que estado del juego nos encontramos
    public GameState gameState = GameState.inMenu;

    public Canvas menuCanvas, inGameCanvas,gameOverCanvas;

    public int collectedCoin = 0;

    //AudioClips para cambio de musica
    public AudioClip inMenuTrack;
    public AudioClip inGameTrack;
    //private MusicManager theAM;

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        BackToMenu();
        //Buscar objeto que tiene la musica
        //theAM = FindObjectOfType<MusicManager>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Start") && this.gameState != GameState.inGame)
        {
            StartGame();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            BackToMenu();
        }
    }

    public void StartGame()
    {
        setGameState(GameState.inGame);

        CameraFollow.sharedInstance.ResetCameraPosition();

        if (PlayerContoller.sharedInstance.transform.position.x > 10)
        {
            LevelGenerator.sharedInstance.RemoveAllTheBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlock();
        }
        PlayerContoller.sharedInstance.StartPlayer();
        this.collectedCoin = 0;
    }

    public void GameOver()
    {
        setGameState(GameState.inGameOver);
    }

    public void BackToMenu()
    {
        setGameState(GameState.inMenu);
    }

    public void ExitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif    
    }

    void setGameState(GameState newGameState)
    {
        if (newGameState == GameState.inGame)
        {
            //Se prepara la escena para jugar 
            menuCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            inGameCanvas.enabled = true;

            //Musica
            if(inGameTrack!=null){
                //theAM.ChangeBGM(inGameTrack);
                MusicManager.sharedInstance.ChangeBGM(inGameTrack);
            }

        }
        else if (newGameState == GameState.inGameOver)
        {
            //Se prepara la escena para GameOver
            menuCanvas.enabled = false;
            gameOverCanvas.enabled = true;
            inGameCanvas.enabled = false;
            //Musica
            if(inMenuTrack!=null){
                //theAM.ChangeBGM(inMenuTrack);
                MusicManager.sharedInstance.ChangeBGM(inMenuTrack);
            }

        }
        else if (newGameState == GameState.inMenu)
        {
            menuCanvas.enabled = true;
            gameOverCanvas.enabled = false;
            inGameCanvas.enabled = false;
        }
        this.gameState = newGameState;
    }


    public void CollectCoins(int coinValue){
        this.collectedCoin += coinValue;

        Debug.Log("Llevamos recogidos: "+ this.collectedCoin); 
    }
}
