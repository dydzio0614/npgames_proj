using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event Action OnGameEnded;
    
    [field: SerializeField]
    public Transform BulletsContainer { get; private set; }

    [SerializeField]
    private GameObject simulationObjectPrefab;
    [SerializeField]
    private Transform simulationObjectsContainer;

    private int currentObjectsQuantity;
    
    private void Awake()
    {
        if(Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    public void StartGame(int simulationObjectsQuantity)
    {
        currentObjectsQuantity = simulationObjectsQuantity;
        
        List<Vector2> possibleSpawnPoints = GenerateSpawnPositionsList();
        System.Random random = new System.Random();
        
        for (int i = 0; i < simulationObjectsQuantity; i++)
        {
            int spawnSpotIndex = random.Next(possibleSpawnPoints.Count);
            Vector2 spawnSpot = possibleSpawnPoints[spawnSpotIndex];
            possibleSpawnPoints.RemoveAt(spawnSpotIndex);
            
            var spawnedObject = Instantiate(simulationObjectPrefab,
                new Vector3(UnityEngine.Random.Range(spawnSpot.x - 0.25f, spawnSpot.x + 0.25f), 0.5f, UnityEngine.Random.Range(spawnSpot.y - 0.25f, spawnSpot.y + 0.25f)), Quaternion.identity, simulationObjectsContainer);
            spawnedObject.GetComponent<SimulationObjectController>().OnDeath += SimulationObjectDeathEventHandler;
        }
    }

    private List<Vector2> GenerateSpawnPositionsList()
    {
        List<Vector2> result = new List<Vector2>(); 
        
        //assumptions: starting positions and spots density based on current viewport, safe position size based on 1f width/length object size
        const float xStart = -55f;
        const float yStart = -30f;
        const float dimensionSize = 2f;
        const int numberOfRows = 30;
        const int numberOfColumns = 55;

        for (int i = 0; i < numberOfRows; i++)
            for (int j = 0; j < numberOfColumns; j++)
                result.Add(new Vector2(xStart + j * dimensionSize, yStart + i * dimensionSize));

        return result;
    }

    private void SimulationObjectDeathEventHandler()
    {
        currentObjectsQuantity -= 1;

        if (currentObjectsQuantity <= 1)
        {
            EndGame();
        }    
    }

    private void EndGame()
    {
        foreach(Transform child in simulationObjectsContainer.transform)
            Destroy(child.gameObject);
        
        foreach(Transform child in BulletsContainer.transform)
            Destroy(child.gameObject);
        
        OnGameEnded?.Invoke();
    }
}
