using Mirror;
using UnityEngine;

/// <summary>
/// DriverController manages the behavior and data synchronization for the driver client.
/// This script handles keyboard-based movement, local wheel rotation simulation, 
/// synchronization of driver-related data (e.g., position, rotation, wheel data),
/// and processes data received from other clients (e.g., pedestrians and drivers).
/// </summary>
public class DriverController: NetworkBehaviour
{
    [Header("Driver Configuration")]
    public Transform driverObject;    // The main driver object
    public Transform pedestrianObject; // Pedestrian object to represent a pedestrian in the environment
    public Transform cyclistObject;    // Cyclist object to represent a cyclist in the environment

    [Header("Driver Wheels")]
    public Transform wheel_FL;        // Front-left wheel
    public Transform wheel_FR;        // Front-right wheel
    public Transform wheel_BL;        // Back-left wheel
    public Transform wheel_BR;        // Back-right wheel

    [Header("Movement Settings")]
    public float moveSpeed = 10f;     // Speed for forward/backward movement
    public float rotateSpeed = 50f;   // Speed for rotation

    private void Start()
    {
        // Log connection status for debugging
        if (!NetworkClient.isConnected)
        {
            Debug.LogWarning("Client is not connected to the server yet.");
        } else
        {
            Debug.Log("Client successfully connected to the server.");
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            // Handle movement using keyboard input
            HandleKeyboardInput();

            // Simulate wheel rotations based on movement
            SimulateWheelRotation();

            // Send driver's position and wheel data to the server
            CmdSendDriverData(
                driverObject.position,
                driverObject.rotation,
                wheel_FL.rotation,
                wheel_FR.rotation,
                wheel_BL.rotation,
                wheel_BR.rotation
            );
        }
    }

    /// <summary>
    /// Handles keyboard input for movement and rotation of the driver object.
    /// </summary>
    private void HandleKeyboardInput()
    {
        // Get input for movement (W/S or UpArrow/DownArrow)
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Get input for rotation (A/D or LeftArrow/RightArrow)
        float rotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

        // Apply movement and rotation to the driver object
        driverObject.Translate(0, 0, move);
        driverObject.Rotate(0, rotate, 0);
    }

    /// <summary>
    /// Simulates the rotation of the wheels based on movement.
    /// </summary>
    private void SimulateWheelRotation()
    {
        float rotationSpeed = 200f * Time.deltaTime;

        // Rotate each wheel along the local X-axis
        wheel_FL.Rotate(Vector3.right, rotationSpeed);
        wheel_FR.Rotate(Vector3.right, rotationSpeed);
        wheel_BL.Rotate(Vector3.right, rotationSpeed);
        wheel_BR.Rotate(Vector3.right, rotationSpeed);
    }

    /// <summary>
    /// Sends driver data (position, rotation, and wheel rotations) to the server for broadcasting.
    /// </summary>
    /// <param name="position">Driver's current position</param>
    /// <param name="rotation">Driver's current rotation</param>
    /// <param name="wheelFLRot">Front-left wheel rotation</param>
    /// <param name="wheelFRRot">Front-right wheel rotation</param>
    /// <param name="wheelBLRot">Back-left wheel rotation</param>
    /// <param name="wheelBRRot">Back-right wheel rotation</param>
    [Command]
    private void CmdSendDriverData(Vector3 position, Quaternion rotation, Quaternion wheelFLRot, Quaternion wheelFRRot, Quaternion wheelBLRot, Quaternion wheelBRRot)
    {
        if (!NetworkClient.isConnected)
        {
            Debug.LogWarning("Client is not connected to the server. Data will not be sent.");
            return;
        }

        // Create a message containing driver data
        DriverDataMessage msg = new DriverDataMessage
        {
            clientId = netId.ToString(),
            position = position,
            rotation = rotation,
            wheelFLRotation = wheelFLRot,
            wheelFRRotation = wheelFRRot,
            wheelBLRotation = wheelBLRot,
            wheelBRRotation = wheelBRRot
        };

        // Broadcast data to all clients via the server
        NetworkServer.SendToAll(msg);
    }

    private void OnEnable()
    {
        if (isClient)
        {
            // Register handlers for receiving data from the server
            NetworkClient.RegisterHandler<PedestrianDataChunkMessage>(OnPedestrianDataChunkReceived);
            NetworkClient.RegisterHandler<BicycleDataMessage>(OnBicycleDataReceived);
            NetworkClient.RegisterHandler<DriverDataMessage>(OnDriverDataReceived);
        }
    }

    /// <summary>
    /// Handles received pedestrian data chunks and updates the pedestrian object.
    /// </summary>
    private void OnPedestrianDataChunkReceived(PedestrianDataChunkMessage msg)
    {
        if (msg.clientId == netId.ToString()) return; // Ignore self-data

        if (msg.positions == null || msg.rotations == null)
        {
            Debug.LogWarning("Received null pedestrian data chunk. Ignoring...");
            return;
        }

        // Update pedestrian joint data
        for (int i = 0; i < msg.positions.Length; i++)
        {
            int jointIndex = msg.startIndex + i;
            if (jointIndex < pedestrianObject.childCount)
            {
                Transform joint = pedestrianObject.GetChild(jointIndex);
                joint.position = msg.positions[i];
                joint.rotation = msg.rotations[i];
            }
        }
    }

    /// <summary>
    /// Handles received bicycle data and updates the cyclist object.
    /// </summary>
    private void OnBicycleDataReceived(BicycleDataMessage msg)
    {
        if (msg.position == Vector3.zero && msg.rotation == Quaternion.identity)
        {
            Debug.LogWarning("Received empty bicycle data. Skipping...");
            return;
        }

        cyclistObject.position = msg.position;
        cyclistObject.rotation = msg.rotation;

        // Update bicycle wheels and pedals
        Transform wheel_B = cyclistObject.Find("Wheel_B");
        Transform wheel_F = cyclistObject.Find("Wheel_F");
        Transform pedal = cyclistObject.Find("Pedal");
        Transform pedal_L = cyclistObject.Find("Pedal_L");
        Transform pedal_R = cyclistObject.Find("Pedal_R");

        if (wheel_B) wheel_B.rotation = msg.wheelBRotation;
        if (wheel_F) wheel_F.rotation = msg.wheelFRotation;
        if (pedal) pedal.rotation = msg.pedalRotation;
        if (pedal_L) pedal_L.rotation = msg.pedalLRotation;
        if (pedal_R) pedal_R.rotation = msg.pedalRRotation;
    }

    /// <summary>
    /// Handles received driver data from other driver clients (if any).
    /// </summary>
    private void OnDriverDataReceived(DriverDataMessage msg)
    {
        if (msg.clientId == netId.ToString()) return; // Ignore self-data
    }
}
