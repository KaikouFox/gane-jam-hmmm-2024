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
        Controls,
    }

    [SerializeField] private GameObject actionText;
    [SerializeField] private GameObject scrapText;
    [SerializeField] private GameObject dayText;
    [SerializeField] private GameObject endingText;
    [SerializeField] private GameObject announcementText;
    [SerializeField] private GameObject defenceText;
    [SerializeField] private GameObject subText;
    [SerializeField] private List<GameObject> screens;

    [Header("Sliders")]

    private CanvasState currentState;
    private CanvasState previousState;

    private void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape) && currentState != CanvasState.Controls)
        {
            previousState = currentState;
            SetCanvasState(CanvasState.Controls);
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCanvasState(previousState);
        }
    }

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

            case CanvasState.Controls:
                screens[2].SetActive(true);
                break;
        }
    }

    public void SetActionPoints(int amount)
    {
        actionText.GetComponent<TextMeshProUGUI>().text = "Actionpoints: " + amount;
    }

    public void SetDefencePoints(int amount)
    {
        defenceText.GetComponent<TextMeshProUGUI>().text = "Defencepoints: " + amount;
    }

    public void SetDay(int amount)
    {
        dayText.GetComponent<TextMeshProUGUI>().text = "Day: " + amount;
    }
    
    public void SetScrapPoints(int amount)
    {
        scrapText.GetComponent<TextMeshProUGUI>().text = "Scrap: " + amount;
    }

    public void SetEnding(string text, Color textColor, string subtext)
    {
        endingText.GetComponent<TextMeshProUGUI>().text = text;
        endingText.GetComponent<TextMeshProUGUI>().color = textColor;
        subText.GetComponent<TextMeshProUGUI>().text = subtext;
        SetCanvasState(CanvasState.Ending);
    }

    public void SetAnnouncement(string message)
    {
        Debug.Log(message);
        announcementText.GetComponent<TextMeshProUGUI>().text = message;
    }
}
