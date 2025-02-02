using Mirror;
using UnityEngine;

/// <summary>
/// AVController manages the behavior of an autonomous vehicle (AV) in various eHMI modes.
/// This script handles:
/// - Keyboard-based control for movement and deceleration.
/// - Trigger-based behavior for different AV modes (Yield, NonYield, WhomEHMI).
/// - Communication with the server to synchronize speed and control states.
/// </summary>
public class AVController: NetworkBehaviour
{
    /// <summary>
    /// Enumeration for AV modes:
    /// - Yield: The AV yields to other road users.
    /// - NonYield: The AV continues driving without yielding.
    /// </summary>
    public enum AVMode { Yield, NonYield, WhomEHMI }
    public AVMode currentMode; // Current mode of the AV, set by the server or experiment manager.

    [Header("AV Components")]
    public GameObject eHMI; // External Human-Machine Interface (eHMI) object.
    public GameObject CollisionDetector; // Collider used to detect interactions with road users.

    [Header("Driving Parameters")]
    private float currentSpeed = 0f; // Current speed of the AV.
    private float maxSpeed = 10f; // Maximum speed the AV can reach.
    private float acceleration = 2f; // Acceleration rate.
    private float deceleration = 2f; // Deceleration rate.
    private bool isDriving = false; // Indicates whether the AV is currently driving.
    private bool isDecelerating = false; // Indicates whether the AV is decelerating.

    /// <summary>
    /// Initializes the AVController. Registers client-side message handlers.
    /// </summary>
    private void Start()
    {
        if (isClient)
        {
            // Register message handlers for speed and control messages.
            NetworkClient.RegisterHandler<SpeedMessage>(OnSpeedMessageReceived);
            NetworkClient.RegisterHandler<ControlMessage>(OnControlMessageReceived);
        }
    }

    /// <summary>
    /// Updates the driving state of the AV based on the current mode.
    /// </summary>
    private void Update()
    {
        if (isDriving)
        {
            if (isDecelerating)
            {
                Decelerate();
            } else
            {
                Drive();
            }
        }
    }

    /// <summary>
    /// Starts driving the AV.
    /// </summary>
    public void StartAVDriving()
    {
        isDriving = true;
    }

    /// <summary>
    /// Stops the AV.
    /// </summary>
    public void StopAV()
    {
        isDriving = false;
        isDecelerating = false;
    }

    /// <summary>
    /// Drives the AV forward at the current speed.
    /// </summary>
    private void Drive()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Decelerates the AV until it comes to a complete stop.
    /// </summary>
    private void Decelerate()
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (currentSpeed <= 0f)
        {
            isDecelerating = false;
            StopAV();
        }
    }

    /// <summary>
    /// Activates the eHMI to communicate the AV's intentions.
    /// </summary>
    private void ActivateEHMI()
    {
        if (eHMI != null)
        {
            eHMI.SetActive(true);
        }
    }

    /// <summary>
    /// Deactivates the eHMI.
    /// </summary>
    private void DeactivateEHMI()
    {
        if (eHMI != null)
        {
            eHMI.SetActive(false);
        }
    }

    /// <summary>
    /// Handles the behavior when the AV collides with the CollisionDetector.
    /// </summary>
    /// <param name="other">The collider object.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == CollisionDetector)
        {
            switch (currentMode)
            {
                case AVMode.Yield:
                    HandleYieldMode();
                    break;

                case AVMode.NonYield:
                    HandleNonYieldMode();
                    break;

                case AVMode.WhomEHMI:
                    HandleWhomEHMI();
                    break;
            }
        }
    }

    /// <summary>
    /// Handles the behavior when the AV exits the CollisionDetector area.
    /// </summary>
    /// <param name="other">The collider object.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == CollisionDetector)
        {
            if (currentMode == AVMode.Yield || currentMode == AVMode.WhomEHMI)
            {
                DeactivateEHMI();
            }
        }
    }

    /// <summary>
    /// Handles Yield mode: decelerates and activates eHMI.
    /// </summary>
    private void HandleYieldMode()
    {
        isDecelerating = true;
        ActivateEHMI();
    }

    /// <summary>
    /// Handles NonYield mode: continues driving without activating eHMI.
    /// </summary>
    private void HandleNonYieldMode()
    {
        isDriving = true;
        isDecelerating = false;
        DeactivateEHMI();
    }

    /// <summary>
    /// Handles WhomEHMI mode: activates eHMI without deceleration.
    /// </summary>
    private void HandleWhomEHMI()
    {
        ActivateEHMI();
    }

    /// <summary>
    /// Processes the SpeedMessage received from the server.
    /// </summary>
    /// <param name="msg">The speed message.</param>
    private void OnSpeedMessageReceived(SpeedMessage msg)
    {
        currentSpeed = msg.speed;
    }

    /// <summary>
    /// Processes the ControlMessage received from the server.
    /// </summary>
    /// <param name="msg">The control message.</param>
    private void OnControlMessageReceived(ControlMessage msg)
    {
        if (msg.isDriving)
        {
            StartAVDriving();
        } else
        {
            StopAV();
        }
    }
}

/// <summary>
/// Structure for synchronizing the speed of the AV with the server.
/// </summary>
public struct SpeedMessage: NetworkMessage
{
    public float speed; // The current speed of the AV.
}

/// <summary>
/// Structure for sending start/stop commands from the server.
/// </summary>
public struct ControlMessage: NetworkMessage
{
    public bool isDriving; // Indicates whether the AV should start or stop driving.
}
