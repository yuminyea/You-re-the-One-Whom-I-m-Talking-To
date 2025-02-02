using Mirror;
using UnityEngine;

/// <summary>
/// BicycleController manages the behavior and data synchronization for the cyclist client.
/// This script handles the collection and transmission of bicycle-related data (e.g., position, rotation, and wheel/pedal data),
/// as well as receiving and updating data from other clients (e.g., pedestrians and drivers).
/// </summary>
public class BicycleController: NetworkBehaviour
{
    [Header("Cyclist Configuration")]
    public Transform cyclistObject;    // The main cyclist object
    public Transform pedestrianObject; // Pedestrian object to represent a pedestrian in the environment
    public Transform driverObject;     // Driver object to represent a driver in the environment

    [Header("Bicycle Components")]
    public Transform pedal;            // Main pedal
    public Transform pedal_L;          // Left pedal
    public Transform pedal_R;          // Right pedal
    public Transform wheel_B;          // Back wheel
    public Transform wheel_F;          // Front wheel

    [Header("Movement Settings")]
    public float moveSpeed = 5f;       // Speed of forward/backward movement
    public float rotateSpeed = 100f;   // Speed of rotation

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

            // Simulate wheel and pedal rotations
            RotateWheelsAndPedals();

            // Send cyclist data to the server
            CmdSendBicycleData(
                cyclistObject.position,
                cyclistObject.rotation,
                pedal.rotation,
                pedal_L.rotation,
                pedal_R.rotation,
                wheel_B.rotation,
                wheel_F.rotation
            );
        }
    }

    /// <summary>
    /// Handles keyboard input for cyclist movement.
    /// </summary>
    private void HandleKeyboardInput()
    {
        // Get input for forward/backward movement and rotation
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

        // Apply movement and rotation
        cyclistObject.Translate(0, 0, move);
        cyclistObject.Rotate(0, rotate, 0);
    }

    /// <summary>
    /// Simulates rotation for bicycle wheels and pedals.
    /// </summary>
    private void RotateWheelsAndPedals()
    {
        float rotationSpeed = 200f * Time.deltaTime;
        pedal.Rotate(Vector3.right, rotationSpeed);
        pedal_L.Rotate(Vector3.right, rotationSpeed);
        pedal_R.Rotate(Vector3.right, rotationSpeed);
        wheel_B.Rotate(Vector3.right, rotationSpeed);
        wheel_F.Rotate(Vector3.right, rotationSpeed);
    }

    /// <summary>
    /// Sends cyclist data to the server for broadcasting to other clients.
    /// </summary>
    /// <param name="position">Current position of the cyclist.</param>
    /// <param name="rotation">Current rotation of the cyclist.</param>
    /// <param name="pedalRot">Rotation of the main pedal.</param>
    /// <param name="pedalLRot">Rotation of the left pedal.</param>
    /// <param name="pedalRRot">Rotation of the right pedal.</param>
    /// <param name="wheelBRot">Rotation of the back wheel.</param>
    /// <param name="wheelFRot">Rotation of the front wheel.</param>
    [Command]
    private void CmdSendBicycleData(Vector3 position, Quaternion rotation, Quaternion pedalRot, Quaternion pedalLRot, Quaternion pedalRRot, Quaternion wheelBRot, Quaternion wheelFRot)
    {
        if (!NetworkClient.isConnected)
        {
            Debug.LogWarning("Client is not connected to the server. Data will not be sent.");
            return;
        }

        // Create a message containing cyclist data
        BicycleDataMessage msg = new BicycleDataMessage
        {
            clientId = netId.ToString(),
            position = position,
            rotation = rotation,
            pedalRotation = pedalRot,
            pedalLRotation = pedalLRot,
            pedalRRotation = pedalRRot,
            wheelBRotation = wheelBRot,
            wheelFRotation = wheelFRot
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
    /// Handles received pedestrian data chunks and updates the pedestrian object in the scene.
    /// </summary>
    private void OnPedestrianDataChunkReceived(PedestrianDataChunkMessage msg)
    {
        if (msg.clientId == netId.ToString()) return; // Ignore self-data

        if (msg.positions == null || msg.rotations == null)
        {
            Debug.LogWarning("Received null pedestrian data chunk. Ignoring...");
            return;
        }

        // Update joint data for the pedestrian object
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
    /// Handles received driver data and updates the driver object in the scene.
    /// </summary>
    private void OnDriverDataReceived(DriverDataMessage msg)
    {
        if (msg.position == Vector3.zero && msg.rotation == Quaternion.identity)
        {
            Debug.LogWarning("Received empty driver data. Skipping...");
            return;
        }

        driverObject.position = msg.position;
        driverObject.rotation = msg.rotation;

        // Update driver wheel rotations
        Transform wheel_FL = driverObject.Find("Wheel_FL");
        Transform wheel_FR = driverObject.Find("Wheel_FR");
        Transform wheel_BL = driverObject.Find("Wheel_BL");
        Transform wheel_BR = driverObject.Find("Wheel_BR");

        if (wheel_FL) wheel_FL.rotation = msg.wheelFLRotation;
        if (wheel_FR) wheel_FR.rotation = msg.wheelFRRotation;
        if (wheel_BL) wheel_BL.rotation = msg.wheelBLRotation;
        if (wheel_BR) wheel_BR.rotation = msg.wheelBRRotation;
    }

    /// <summary>
    /// Handles received bicycle data messages (from other cyclist clients).
    /// </summary>
    private void OnBicycleDataReceived(BicycleDataMessage msg)
    {
        if (msg.clientId == netId.ToString()) return; // Ignore self-data
    }
}
