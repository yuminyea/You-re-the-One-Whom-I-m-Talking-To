using Mirror;
using UnityEngine;

/// <summary>
/// Server-side controller to manage autonomous vehicle speed and driving state, and synchronize these states with connected clients.
/// </summary>
public class ServerController: NetworkBehaviour
{
    private float currentSpeed = 0f; // Current speed of the vehicle
    private float targetSpeed = 0f; // Target speed of the vehicle
    private float acceleration = 2f; // Acceleration rate
    private float deceleration = 2f; // Deceleration rate
    private bool isDriving = false; // Driving state (true = driving, false = stopped)

    private void Update()
    {
        if (!isServer) return; // Ensure this code runs only on the server

        // Check for keyboard input to control driving state
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartDriving(); // Start driving when 'S' is pressed
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            StopDriving(); // Stop driving when 'X' is pressed
        }

        // Continuously update the vehicle's speed
        UpdateSpeed();
    }

    /// <summary>
    /// Gradually updates the current speed towards the target speed based on acceleration or deceleration.
    /// </summary>
    private void UpdateSpeed()
    {
        if (isDriving)
        {
            // Accelerate towards the target speed
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        } else
        {
            // Decelerate towards zero
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // Broadcast the current speed to all connected clients
        SendSpeedToClients(currentSpeed);
    }

    /// <summary>
    /// Sends the current speed to all connected clients.
    /// </summary>
    /// <param name="speed">The current speed of the vehicle.</param>
    private void SendSpeedToClients(float speed)
    {
        SpeedMessage speedMessage = new SpeedMessage { speed = speed };
        NetworkServer.SendToAll(speedMessage);
        Debug.Log($"Current speed ({speed}) sent to all clients.");
    }

    /// <summary>
    /// Starts the vehicle driving and notifies all clients.
    /// </summary>
    public void StartDriving()
    {
        isDriving = true;
        targetSpeed = 10f; // Set a desired driving speed
        Debug.Log("Vehicle started driving.");

        // Notify clients of the driving state
        DrivingStateMessage drivingStateMessage = new DrivingStateMessage { isDriving = true };
        NetworkServer.SendToAll(drivingStateMessage);
    }

    /// <summary>
    /// Stops the vehicle driving and notifies all clients.
    /// </summary>
    public void StopDriving()
    {
        isDriving = false;
        Debug.Log("Vehicle stopped driving.");

        // Notify clients of the driving state
        DrivingStateMessage drivingStateMessage = new DrivingStateMessage { isDriving = false };
        NetworkServer.SendToAll(drivingStateMessage);
    }
}

/// <summary>
/// Message structure to send the current speed to clients.
/// </summary>
public struct SpeedMessage: NetworkMessage
{
    public float speed; // Current vehicle speed
}

/// <summary>
/// Message structure to notify clients of the vehicle's driving state.
/// </summary>
public struct DrivingStateMessage: NetworkMessage
{
    public bool isDriving; // True if the vehicle is driving, false if stopped
}
