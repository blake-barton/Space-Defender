using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // cached reference
    TextMeshProUGUI textComponent;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
        UpdateTextDisplay();
    }

    public void UpdateTextDisplay()
    {
        textComponent.text = "SCORE: " + gameSession.GetScore().ToString("D4");
    }
}
