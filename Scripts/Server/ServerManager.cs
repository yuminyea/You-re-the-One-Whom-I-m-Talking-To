using Mirror;
using UnityEngine;

/// <summary>
/// Manages server-side functionality, including registering handlers and broadcasting messages between clients.
/// </summary>
public class ServerManager: NetworkBehaviour
{
    /// <summary>
    /// Registers message handlers for different client data types when the server starts.
    /// </summary>
    private void Start()
    {
        if (isServer)
        {
            // Register handlers for each data type
            NetworkServer.RegisterHandler<PedestrianDataChunkMessage>(OnPedestrianDataChunkReceived);
            NetworkServer.RegisterHandler<BicycleDataMessage>(OnBicycleDataReceived);
            NetworkServer.RegisterHandler<DriverDataMessage>(OnDriverDataReceived);

            Debug.Log("Handlers registered for server.");
        }
    }

    /// <summary>
    /// Processes pedestrian data received from a client and broadcasts it to all other clients.
    /// </summary>
    private void OnPedestrianDataChunkReceived(NetworkConnection conn, PedestrianDataChunkMessage msg)
    {
        // Ensure all clients are connected before broadcasting
        if (NetworkServer.connections.Count < 3)
        {
            Debug.LogWarning("Not all clients are connected. Waiting for all clients to connect...");
            return;
        }

        // Broadcast to all clients except the sender
        NetworkServer.SendToAll(msg, conn.connectionId);
        Debug.Log($"Pedestrian data received from client {msg.clientId} and broadcasted.");
    }

    /// <summary>
    /// Processes bicycle data received from a client and broadcasts it to all other clients.
    /// </summary>
    private void OnBicycleDataReceived(NetworkConnection conn, BicycleDataMessage msg)
    {
        // Ensure all clients are connected before broadcasting
        if (NetworkServer.connections.Count < 3)
        {
            Debug.LogWarning("Not all clients are connected. Waiting for all clients to connect...");
            return;
        }

        // Broadcast to all clients except the sender
        NetworkServer.SendToAll(msg, conn.connectionId);
        Debug.Log($"Bicycle data received from client {msg.clientId} and broadcasted.");
    }

    /// <summary>
    /// Processes driver data received from a client and broadcasts it to all other clients.
    /// </summary>
    private void OnDriverDataReceived(NetworkConnection conn, DriverDataMessage msg)
    {
        // Ensure all clients are connected before broadcasting
        if (NetworkServer.connections.Count < 3)
        {
            Debug.LogWarning("Not all clients are connected. Waiting for all clients to connect...");
            return;
        }

        // Broadcast to all clients except the sender
        NetworkServer.SendToAll(msg, conn.connectionId);
        Debug.Log($"Driver data received from client {msg.clientId} and broadcasted.");
    }
}
