using UnityEngine;
using TMPro;

public class PowerUpUIManager : MonoBehaviour
{
    private IPowerUpEffectSO powerUpData;
    private TextMeshPro powerUpName;
    private TextMeshPro powerUpDescription;

    private void Start()
    {
        powerUpData = GetComponent<PowerUpColisionManager>().PowerUpEffect;
        GetComponent<SpriteRenderer>().sprite = powerUpData.Icon;
        Canvas canvas = GetComponentInChildren<Canvas>();
        TextMeshPro[] textArray = canvas.GetComponentsInChildren<TextMeshPro>();
        powerUpName = textArray[0].GetComponent<TextMeshPro>();
        powerUpDescription = textArray[1].GetComponent<TextMeshPro>();

        if (powerUpData != null)
        {
            powerUpName.text = powerUpData.Name;
            powerUpDescription.text = powerUpData.Description;
        }
    }
}