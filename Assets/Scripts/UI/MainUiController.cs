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
}
