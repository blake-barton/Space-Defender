using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldCountUI : MonoBehaviour
{
    // cached reference
    TextMeshProUGUI textComponent;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateShieldCountText(int health)
    {
        textComponent.text = health.ToString();
    }
}
