using Mirror;
using UnityEngine;

/// <summary>
/// PedestrianController manages the behavior and data synchronization for the pedestrian client.
/// This script handles the collection and transmission of joint data, as well as receiving data
/// from other clients (e.g., cyclist and driver).
/// </summary>
public class PedestrianController: NetworkBehaviour
{
    [Header("Pedestrian Configuration")]
    public Transform[] jointObjects; // Array of joints (e.g., Hips, Spine, Head, etc.)
    public Transform cyclistObject; // Bicycle object to represent cyclist in the environment
    public Transform driverObject;  // Driver object to represent driver in the environment

    [Header("Movement Settings")]
    public float moveSpeed = 5f;    // Speed of forward/backward movement
    public float rotateSpeed = 100f; // Speed of rotation
    private const int ChunkSize = 3; // Number of joints to send in each data chunk

    private void Start()
    {
        // Log the client connection status
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

            // Collect and send joint data in chunks
            SendJointDataInChunks();
        }
    }

    /// <summary>
    /// Handles keyboard input for pedestrian movement.
    /// </summary>
    private void HandleKeyboardInput()
    {
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // Forward/backward
        float rotate = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime; // Rotation

        // Apply movement and rotation
        transform.Translate(0, 0, move);
        transform.Rotate(0, rotate, 0);
    }

    /// <summary>
    /// Collects joint data and sends it to the server in chunks.
    /// </summary>
    private void SendJointDataInChunks()
    {
        Vector3[] jointPositions = new Vector3[jointObjects.Length];
        Quaternion[] jointRotations = new Quaternion[jointObjects.Length];

        // Collect joint positions and rotations
        for (int i = 0; i < jointObjects.Length; i++)
        {
            jointPositions[i] = jointObjects[i].position;
            jointRotations[i] = jointObjects[i].rotation;
        }

        // Send data in chunks of defined size
        for (int i = 0; i < jointObjects.Length; i += ChunkSize)
        {
            int chunkLength = Mathf.Min(ChunkSize, jointObjects.Length - i);
            Vector3[] positionChunk = new Vector3[chunkLength];
            Quaternion[] rotationChunk = new Quaternion[chunkLength];

            System.Array.Copy(jointPositions, i, positionChunk, 0, chunkLength);
            System.Array.Copy(jointRotations, i, rotationChunk, 0, chunkLength);

            CmdSendPedestrianDataChunk(i, positionChunk, rotationChunk);
        }
    }

    /// <summary>
    /// Sends a chunk of pedestrian joint data to the server.
    /// </summary>
    /// <param name="startIndex">The starting index of the chunk.</param>
    /// <param name="positions">The joint positions in the chunk.</param>
    /// <param name="rotations">The joint rotations in the chunk.</param>
    [Command]
    private void CmdSendPedestrianDataChunk(int startIndex, Vector3[] positions, Quaternion[] rotations)
    {
        if (!NetworkClient.isConnected)
        {
            Debug.LogWarning("Client is not connected to the server. Data will not be sent.");
            return;
        }

        // Create the message and send it to all clients via the server
        PedestrianDataChunkMessage msg = new PedestrianDataChunkMessage
        {
            clientId = netId.ToString(),
            startIndex = startIndex,
            positions = positions,
            rotations = rotations
        };
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
    /// Handles received pedestrian data chunk messages.
    /// </summary>
    private void OnPedestrianDataChunkReceived(PedestrianDataChunkMessage msg)
    {
        if (msg.clientId == netId.ToString()) return; // Ignore data from self

        if (msg.positions == null || msg.rotations == null)
        {
            Debug.LogWarning("Received null pedestrian data chunk. Ignoring...");
            return;
        }

        // Update joint data (if necessary)
    }

    /// <summary>
    /// Handles received bicycle data messages.
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
    /// Handles received driver data messages.
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

        // Update driver wheels
        Transform wheel_FL = driverObject.Find("Wheel_FL");
        Transform wheel_FR = driverObject.Find("Wheel_FR");
        Transform wheel_BL = driverObject.Find("Wheel_BL");
        Transform wheel_BR = driverObject.Find("Wheel_BR");

        if (wheel_FL) wheel_FL.rotation = msg.wheelFLRotation;
        if (wheel_FR) wheel_FR.rotation = msg.wheelFRRotation;
        if (wheel_BL) wheel_BL.rotation = msg.wheelBLRotation;
        if (wheel_BR) wheel_BR.rotation = msg.wheelBRRotation;
    }
}
