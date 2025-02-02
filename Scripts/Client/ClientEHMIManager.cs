using Mirror;
using UnityEngine;

/// <summary>
/// This script manages eHMI (external Human-Machine Interface) states for the client.
/// It listens for server messages to update and display the appropriate eHMI condition.
/// </summary>
public class ClientEHMIManager: NetworkBehaviour
{
    [Header("eHMI States")]
    [Tooltip("GameObject representing no eHMI state.")]
    public GameObject no_eHMI;

    [Tooltip("GameObject representing 'Yield, No Context' eHMI state.")]
    public GameObject yield_noContext;

    [Tooltip("GameObject representing 'Nonyield, No Context' eHMI state.")]
    public GameObject nonyield_noContext;

    [Tooltip("GameObject representing 'Yield, Pedestrian (Whom)' eHMI state.")]
    public GameObject yield_ped_whom;

    [Tooltip("GameObject representing 'Yield, Driver (Whom)' eHMI state.")]
    public GameObject yield_driver_whom;

    [Tooltip("GameObject representing 'Yield, Cyclist (Whom)' eHMI state.")]
    public GameObject yield_cyclist_whom;

    [Tooltip("GameObject representing 'Nonyield (Whom)' eHMI state.")]
    public GameObject nonyield_whom;

    [Tooltip("GameObject representing 'Yield, When' eHMI state.")]
    public GameObject yield_when;

    [Tooltip("GameObject representing 'Nonyield, When' eHMI state.")]
    public GameObject nonyield_when;

    [Tooltip("GameObject representing 'Yield, Where' eHMI state.")]
    public GameObject yield_where;

    [Tooltip("GameObject representing 'Nonyield, Where' eHMI state.")]
    public GameObject nonyield_where;

    [Header("Display Object")]
    [Tooltip("The main display object for visualizing the eHMI states.")]
    public GameObject display;

    /// <summary>
    /// Register the eHMI message handler when the client starts.
    /// </summary>
    private void Awake()
    {
        if (!isClient) return;

        // Register the message handler to receive eHMI state updates from the server
        NetworkClient.RegisterHandler<EHMIMessage>(OnConditionReceived);
    }

    /// <summary>
    /// Ensures all eHMI objects are disabled initially.
    /// </summary>
    private void Start()
    {
        DisableAllEHMI();
    }

    /// <summary>
    /// Handles the eHMI condition message received from the server.
    /// </summary>
    /// <param name="msg">The message containing the eHMI condition number.</param>
    private void OnConditionReceived(EHMIMessage msg)
    {
        Debug.Log($"Received condition from server: {msg.conditionNum}");
        ApplyCondition(msg.conditionNum);
    }

    /// <summary>
    /// Applies the specified eHMI condition by enabling the corresponding eHMI state object.
    /// </summary>
    /// <param name="conditionNum">The condition number received from the server.</param>
    private void ApplyCondition(int conditionNum)
    {
        // Disable all eHMI objects before activating the relevant one
        DisableAllEHMI();

        // Select the eHMI object based on the condition number
        GameObject targetEHMI = conditionNum switch
        {
            1 or 2 => no_eHMI,
            3 => yield_noContext,
            4 => nonyield_noContext,
            5 => yield_ped_whom,
            6 => yield_driver_whom,
            7 => yield_cyclist_whom,
            8 => nonyield_whom,
            9 => yield_when,
            10 => nonyield_when,
            11 => yield_where,
            12 => nonyield_where,
            _ => null
        };

        if (targetEHMI != null)
        {
            // Activate the selected eHMI object and the display
            targetEHMI.SetActive(true);
            display.SetActive(true);
        } else
        {
            Debug.LogWarning("Invalid condition number received.");
        }
    }

    /// <summary>
    /// Disables all eHMI state objects and the display.
    /// </summary>
    private void DisableAllEHMI()
    {
        no_eHMI.SetActive(false);
        yield_noContext.SetActive(false);
        nonyield_noContext.SetActive(false);
        yield_ped_whom.SetActive(false);
        yield_driver_whom.SetActive(false);
        yield_cyclist_whom.SetActive(false);
        nonyield_whom.SetActive(false);
        yield_when.SetActive(false);
        nonyield_when.SetActive(false);
        yield_where.SetActive(false);
        nonyield_where.SetActive(false);
        display.SetActive(false); // Ensure display is also disabled
    }
}

/// <summary>
/// Network message structure for eHMI state updates.
/// </summary>
public struct EHMIMessage: NetworkMessage
{
    public int conditionNum; // The condition number sent by the server
}
