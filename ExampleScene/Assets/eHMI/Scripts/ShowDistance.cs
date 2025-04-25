using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the distance between the AV's front and the target on a UI text.
/// </summary>
public class ShowDistance: MonoBehaviour
{
    public GameObject collisionDetector; // Target object to measure distance from
    public GameObject front;             // Reference point on the AV
    public Text distanceText;            // UI element to display the distance

    private bool isActive = false;

    private void OnEnable()
    {
        isActive = true;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    void Update()
    {
        if (!isActive) return;

        if (collisionDetector != null && distanceText != null)
        {
            float distance = Vector3.Distance(front.transform.position, collisionDetector.transform.position);
            int distanceToInt = Mathf.RoundToInt(distance);
            distanceText.text = distanceToInt.ToString();

            if (distanceToInt < 1)
            {
                distanceText.text = "0";
                isActive = false;
            }
        }
    }
}