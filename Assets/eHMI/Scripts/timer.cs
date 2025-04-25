using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Countdown timer for the When eHMI condition.
/// Visually indicates how much time is left until the AV stops.
/// </summary>
public class timer: MonoBehaviour
{
    public float time;            // Total countdown duration
    private float fillAmount = 1; // Current fill level
    private Image myImage;        // UI Image component
    private bool timerActive = false;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        timerActive = true;
        fillAmount = 1;
        myImage.fillAmount = fillAmount;
    }

    private void OnDisable()
    {
        timerActive = false;
    }

    void Update()
    {
        if (timerActive && fillAmount > 0)
        {
            fillAmount -= Time.deltaTime / time;
            myImage.fillAmount = fillAmount;
        }
    }
}
