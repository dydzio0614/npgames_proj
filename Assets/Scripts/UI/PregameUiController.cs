using System;
using UnityEngine;
using UnityEngine.UI;

public class PregameUiController : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button[] quantityButtons;
    [SerializeField]
    private GameObject startButtonDpadIcon;
    [SerializeField]
    private Sprite quantityButtonNormalSprite;
    [SerializeField]
    private Sprite quantityButtonSelectedSprite;
    [SerializeField]
    private float selectedButtonScaleFactor;
    
    private int? objectsQuantity;

    private void Awake()
    {
        startButton.onClick.AddListener(StartButtonEventHandler);
        
        for (var index = 0; index < quantityButtons.Length; index++)
        {
            int quantityToAssign = index switch
            {
                0 => 50,
                1 => 100,
                2 => 250,
                3 => 500,
                _ => throw new InvalidOperationException(
                $"{nameof(PregameUiController)}.{nameof(Awake)}: Unexpected amount of quantity buttons")
            };
            
            var button = quantityButtons[index];
            button.onClick.AddListener(() => QuantityButtonEventHandler(button, quantityToAssign));
        }
        
        ResetState();
    }

    private void StartButtonEventHandler()
    {
        if (!objectsQuantity.HasValue)
            throw new InvalidOperationException(
                $"{nameof(PregameUiController)}.{nameof(StartButtonEventHandler)}: Unexpected amount of quantity buttons");
        
        gameObject.SetActive(false);
        ResetState();
    }

    private void QuantityButtonEventHandler(Button senderButton, int amount)
    {
        ResetQuantityButtonsUi();

        senderButton.transform.localScale = new Vector3(selectedButtonScaleFactor, selectedButtonScaleFactor, selectedButtonScaleFactor);
        senderButton.GetComponent<Image>().sprite = quantityButtonSelectedSprite;
        
        objectsQuantity = amount;
        startButton.interactable = true;
        startButtonDpadIcon.SetActive(true);
    }

    private void ResetQuantityButtonsUi()
    {
        foreach (var quantityButton in quantityButtons)
        {
            quantityButton.transform.localScale = Vector3.one;
            quantityButton.GetComponent<Image>().sprite = quantityButtonNormalSprite;
        }
    }

    private void ResetState()
    {
        ResetQuantityButtonsUi();
        startButton.interactable = false;
        startButtonDpadIcon.SetActive(false);
        objectsQuantity = null;
    }
}
