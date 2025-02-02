using System.IO;
using UnityEngine;
using Mirror;

/// <summary>
/// Manages experimental conditions on the server, allowing condition changes and broadcasting them to connected clients.
/// </summary>
public class expManager: NetworkBehaviour
{
    [SerializeField] private int conditionNum; // Current experimental condition number

    private string logFilePath; // Path for the log file

    /// <summary>
    /// Message structure for broadcasting the eHMI condition to clients.
    /// </summary>
    public struct EHMIMessage: NetworkMessage
    {
        public int conditionNum; // The experimental condition number
    }

    /// <summary>
    /// Called when the server starts. Initializes the log file and logs the server startup.
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();
        logFilePath = Path.Combine(Application.persistentDataPath, "server_log.txt"); // Set the log file path
        Debug.Log("Server started and ready for commands."); // Log server start to the console
        LogToFile("Server started."); // Log server start to the file
    }

    /// <summary>
    /// Handles server-side keyboard input for changing experimental conditions.
    /// </summary>
    private void Update()
    {
        if (isServer) // Ensure this runs only on the server
        {
            if (Input.anyKeyDown) // Check if any key was pressed
            {
                // Check for specific key presses and set the corresponding condition
                if (Input.GetKeyDown(KeyCode.Alpha1)) SetCondition(1);
                if (Input.GetKeyDown(KeyCode.Alpha2)) SetCondition(2);
                if (Input.GetKeyDown(KeyCode.Alpha3)) SetCondition(3);
                if (Input.GetKeyDown(KeyCode.Alpha4)) SetCondition(4);
                if (Input.GetKeyDown(KeyCode.Alpha5)) SetCondition(5);
                if (Input.GetKeyDown(KeyCode.Alpha6)) SetCondition(6);
                if (Input.GetKeyDown(KeyCode.Alpha7)) SetCondition(7);
                if (Input.GetKeyDown(KeyCode.Alpha8)) SetCondition(8);
                if (Input.GetKeyDown(KeyCode.Alpha9)) SetCondition(9);
                if (Input.GetKeyDown(KeyCode.Alpha0)) SetCondition(10);
                if (Input.GetKeyDown(KeyCode.Minus)) SetCondition(11); // Example for '-'
                if (Input.GetKeyDown(KeyCode.Equals)) SetCondition(12); // Example for '='
            }
        }
    }

    /// <summary>
    /// Sets the experimental condition and broadcasts it to all clients.
    /// </summary>
    /// <param name="newConditionNum">The condition number to set.</param>
    [Server]
    public void SetCondition(int newConditionNum)
    {
        // Validate the condition number
        if (newConditionNum < 1 || newConditionNum > 12)
        {
            Debug.LogWarning($"Invalid condition number: {newConditionNum}. Must be between 1 and 12."); // Log invalid input
            LogToFile($"Invalid condition number: {newConditionNum}"); // Log invalid input to file
            return;
        }

        conditionNum = newConditionNum; // Update the condition number
        BroadcastCondition(conditionNum); // Broadcast the condition to clients
    }

    /// <summary>
    /// Broadcasts the current condition to all connected clients.
    /// </summary>
    /// <param name="condition">The condition number to broadcast.</param>
    [Server]
    private void BroadcastCondition(int condition)
    {
        EHMIMessage msg = new EHMIMessage { conditionNum = condition }; // Create the message
        NetworkServer.SendToAll(msg); // Broadcast the message to all clients
        Debug.Log($"Broadcasted condition: {condition}"); // Log the broadcast
        LogToFile($"Broadcasted condition: {condition}"); // Log the broadcast to file
    }

    /// <summary>
    /// Logs a message to the server log file.
    /// </summary>
    /// <param name="message">The message to log.</param>
    private void LogToFile(string message)
    {
        // Append the message with a timestamp to the log file
        File.AppendAllText(logFilePath, $"{System.DateTime.Now}: {message}\n");
    }
}
