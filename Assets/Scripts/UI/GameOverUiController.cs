using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUiController : MonoBehaviour
{
    public void GameOverButtonEventHandler()
    {
        gameObject.SetActive(false);
    }
}
