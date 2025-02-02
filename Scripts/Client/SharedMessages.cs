using Mirror;
using UnityEngine;

/// <summary>
/// Message structure for sending chunks of pedestrian joint data.
/// Used to transmit partial data for pedestrians' joint positions and rotations.
/// </summary>
public struct PedestrianDataChunkMessage: NetworkMessage
{
    public string clientId;       // The ID of the client sending the data
    public int startIndex;        // The starting index of the chunk (e.g., for partial updates)
    public Vector3[] positions;   // Chunk of joint positions
    public Quaternion[] rotations; // Chunk of joint rotations
}

/// <summary>
/// Message structure for transmitting bicycle data.
/// Includes position, rotation, and additional data for wheels and pedals.
/// </summary>
public struct BicycleDataMessage: NetworkMessage
{
    public string clientId;            // The ID of the bicycle client sending the data
    public Vector3 position;           // Position of the bicycle object
    public Quaternion rotation;        // Rotation of the bicycle object
    public Quaternion pedalRotation;   // Rotation of the main pedal
    public Quaternion pedalLRotation;  // Rotation of the left pedal
    public Quaternion pedalRRotation;  // Rotation of the right pedal
    public Quaternion wheelBRotation;  // Rotation of the back wheel
    public Quaternion wheelFRotation;  // Rotation of the front wheel
}

/// <summary>
/// Message structure for transmitting driver data.
/// Includes position, rotation, and wheel-specific data for a manual vehicle driver.
/// </summary>
public struct DriverDataMessage: NetworkMessage
{
    public string clientId;            // The ID of the driver client sending the data
    public Vector3 position;           // Position of the driver's vehicle
    public Quaternion rotation;        // Rotation of the driver's vehicle
    public Quaternion wheelFLRotation; // Rotation of the front-left wheel
    public Quaternion wheelFRRotation; // Rotation of the front-right wheel
    public Quaternion wheelBLRotation; // Rotation of the back-left wheel
    public Quaternion wheelBRRotation; // Rotation of the back-right wheel
}
