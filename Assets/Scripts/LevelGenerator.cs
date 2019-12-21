using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    //Patron singleton
    public static LevelGenerator sharedInstance;
    //Primer bloque en concreto
    public LevelBlock firstLevelBlock;
    //Todos los bloques disponibles
    public List<LevelBlock> allTheLevelBLocks = new List<LevelBlock>();
    //Posicion de generacion del primer bloque
    public Transform levelStartPoint;
    //Bloques actuales en la escena
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    void Awake()
    {
        sharedInstance = this;
    }

    void Start()
    {
        GenerateInitialBlock();
    }

    void Update()
    {

    }

    public void AddLevelBlock()
    {
        int randomIndex = Random.Range(0, allTheLevelBLocks.Count);
        LevelBlock currentBlock;

        Vector3 spawnPosition = Vector3.zero;

        if (currentLevelBlocks.Count == 0)
        {
            currentBlock = (LevelBlock)Instantiate(firstLevelBlock);
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = levelStartPoint.position;
        }
        else
        {
            currentBlock = (LevelBlock)Instantiate(allTheLevelBLocks[randomIndex]);
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position
                            - currentBlock.startPoint.position;
        }

        currentBlock.transform.position = spawnPosition;
        currentLevelBlocks.Add(currentBlock);

    }

    public void RemoveOldestLevelBlock()
    {
        Debug.Log("VOy a destruir");
        LevelBlock oldestLevelBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldestLevelBlock);
        Destroy(oldestLevelBlock.gameObject);
    }

    public void RemoveAllTheBlocks()
    {
        while (currentLevelBlocks.Count > 0)
        {
            RemoveOldestLevelBlock();
        }
    }

    public void GenerateInitialBlock()
    {
        for (int i = 0; i < 4; i++)
        {
            AddLevelBlock();
        }
    }



}
