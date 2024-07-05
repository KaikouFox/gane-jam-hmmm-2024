using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public enum CanvasState
    {
        InGame,
        Ending,
    }

    public enum BarType
    {
        
    }

    [SerializeField] private GameObject actionText;
    [SerializeField] private GameObject scrapText;
    [SerializeField] private GameObject endingText;
    [SerializeField] private List<GameObject> screens;

    [Header("Sliders")]

    private CanvasState currentState;

    public void SetCanvasState(CanvasState state)
    {
        currentState = state;
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }

        switch (state)
        {
            case CanvasState.InGame:
                screens[0].SetActive(true);
                break;

            case CanvasState.Ending:
                screens[1].SetActive(true);
                break;
        }
    }

    public void SetMaxBarValue(float newValue, BarType type)
    {
        switch (type)
        {
            
        }
    }

    public void SetBarValue(float newValue, BarType type)
    {
        switch (type)
        {
            
        }
    }

    public void SetActionPoints(int amount)
    {
        actionText.GetComponent<TextMeshProUGUI>().text = "Actionpoints: " + amount;
    }
    public void SetScrapPoints(int amount)
    {
        scrapText.GetComponent<TextMeshProUGUI>().text = "Scrap: " + amount;
    }

    public void SetEnding(string text, Color textColor)
    {
        endingText.GetComponent<TextMeshProUGUI>().text = text;
        endingText.GetComponent<TextMeshProUGUI>().color = textColor;
        SetCanvasState(CanvasState.Ending);
    }
}
