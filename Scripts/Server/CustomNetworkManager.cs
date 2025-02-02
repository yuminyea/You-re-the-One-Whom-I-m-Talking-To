using Mirror;
using kcp2k;
using UnityEngine;
using System;

/// <summary>
/// Custom NetworkManager for handling server and client-specific logic with KCP transport.
/// </summary>
public class CustomNetworkManager: NetworkManager
{
    [Header("Start Positions")]
    [SerializeField] private Transform pedestrianStart; // Start position for pedestrian client
    [SerializeField] private Transform cyclistStart;    // Start position for cyclist client
    [SerializeField] private Transform driverStart;     // Start position for driver client

    /// <summary>
    /// Called when the server starts. Initializes the server and logs the KCP transport details.
    /// </summary>
    public override void OnStartServer()
    {
        // Cast the transport to KcpTransport
        KcpTransport kcpTransport = transport as KcpTransport;

        if (kcpTransport != null)
        {
            Debug.Log($"Server started on port: {kcpTransport.Port}");
        } else
        {
            Debug.LogError("Transport is not KcpTransport!");
        }

        base.OnStartServer();
    }

    /// <summary>
    /// Called when the client starts. Configures the client to connect to the server using the KCP transport.
    /// </summary>
    public override void OnStartClient()
    {
        // Cast the transport to KcpTransport
        KcpTransport kcpTransport = transport as KcpTransport;

        if (kcpTransport != null)
        {
            // Get server address from command-line arguments
            string serverAddress = CommandLineReader.GetArg("-address") ?? "127.0.0.1"; // Default to localhost
            kcpTransport.ClientConnect(serverAddress);
            Debug.Log($"Client connecting to server at: {serverAddress}:{kcpTransport.Port}");
        } else
        {
            Debug.LogError("Transport is not KcpTransport!");
        }

        base.OnStartClient();
    }

    /// <summary>
    /// Handles adding a player to the server. This can be overridden for specific player assignment logic.
    /// </summary>
    /// <param name="conn">The connection to the client.</param>
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // Default implementation adds the playerPrefab at the first available spawn point.
        base.OnServerAddPlayer(conn);
    }

    /// <summary>
    /// Called when the server stops. Logs the event.
    /// </summary>
    public override void OnStopServer()
    {
        Debug.Log("Server stopped.");
        base.OnStopServer();
    }
}
