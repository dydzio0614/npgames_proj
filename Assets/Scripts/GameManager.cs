using System;
using System.Collections;
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
        
        for (int i = 0; i < simulationObjectsQuantity; i++)
        {
            var spawnedObject = Instantiate(simulationObjectPrefab,
                new Vector3(UnityEngine.Random.Range(-55f, 55f), 0.5f, UnityEngine.Random.Range(-30f, 30f)), Quaternion.identity, simulationObjectsContainer);

            spawnedObject.GetComponent<SimulationObjectController>().OnDeath += SimulationObjectDeathEventHandler;
        }
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
