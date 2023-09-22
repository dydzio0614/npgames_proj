using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUiController : MonoBehaviour
{
    [SerializeField]
    private PregameUiController pregameComponent;

    [SerializeField]
    private GameOverUiController gameOverComponent;

    private void Awake()
    {
        pregameComponent.gameObject.SetActive(true);
        gameOverComponent.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.OnGameEnded += GameEndedEventHandler;
    }

    private void GameEndedEventHandler()
    {
        pregameComponent.gameObject.SetActive(true);
        gameOverComponent.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameEnded -= GameEndedEventHandler;
    }
}
