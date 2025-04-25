using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Visualizes the remaining distance before AV stopping in the Where eHMI condition.
/// </summary>
public class VehicleMoves: MonoBehaviour
{
    public float speed; // Movement speed
    private float timer = 0;
    private bool isActive = false;

    private void OnEnable()
    {
        isActive = true;
        timer = 0;
    }

    private void OnDisable()
    {
        isActive = false;
    }

    void Update()
    {
        if (!isActive) return;

        float move = Time.deltaTime;
        timer += Time.deltaTime;

        if (timer < 2.6f)
        {
            transform.position += Vector3.left * move * speed;
        }
    }
}