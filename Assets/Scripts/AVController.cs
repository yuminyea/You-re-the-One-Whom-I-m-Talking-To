using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// AVController handles autonomous vehicle behavior in a standalone Unity environment.
/// 
/// Core functionalities:
/// - Activates only one contextual eHMI message at a time based on key input (1–0, -, =)
/// - Starts forward driving on Space key press
/// - Gradually decelerates and stops 
/// - Smoothly aligns with a TargetStopPoint as the final stopping point
/// 
/// Used for testing eHMI logic and AV interaction scenarios without networking.
/// </summary>
public class AVController: MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Optional: Displays selected eHMI condition")]
    public UnityEngine.UI.Text conditionText;

    private readonly string[] eHMIConditionNames = {
        "No eHMI (Yield)", "No eHMI (NonYield)", "No Context (Yield)", "No Context (NonYield)",
        "Whom (Pedestrian)", "Whom (Cyclist)", "Whom (Driver)", "Whom (NonYield)",
        "When (Yield)", "When (NonYield)", "Where (Yield)", "Where (NonYield)"
    };

    [Header("eHMI Conditions (General)")]
    public List<GameObject> eHMIs;

    [Header("eHMI Groups (Whom targets)")]
    public GameObject yield_ped, yield_dri, yield_cyc;
    public GameObject nonyield_ped, nonyield_dri, nonyield_cyc;

    [Header("Movement")]
    public GameObject TargetStopPoint;
    public GameObject Obstacle;

    private bool isDriving = false;
    private bool isDecelerating = false;
    private bool eHMISelected = false;
    private bool eHMIActivated = false;
    private int selectedIndex = -1;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float decelerationRate = 1f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        Debug.Log("--- Please select an eHMI condition (1–0, -, =) or numeric keypad equivalent before pressing Space to start the AV. ---");
        foreach (var e in eHMIs)
            if (e != null) e.SetActive(false);

        yield_ped?.SetActive(false);
        yield_dri?.SetActive(false);
        yield_cyc?.SetActive(false);
        nonyield_ped?.SetActive(false);
        nonyield_dri?.SetActive(false);
        nonyield_cyc?.SetActive(false);
    }

    void Update()
    {
        HandleEHMIInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!eHMISelected)
            {
                Debug.LogWarning("--- Select an eHMI condition first (1–0, -, =) before starting the AV ---");
                return;
            }

            isDriving = true;
            Debug.Log("--- AV driving started ---");
        }

        if (isDriving)
        {
            if (isDecelerating && speed > 0.01f)
                transform.position += transform.forward * speed * Time.deltaTime;

            if (isDecelerating)
                DecelerateAndStop();
            else
                transform.position += transform.forward * speed * Time.deltaTime;

            float distance = Vector3.Distance(transform.position, TargetStopPoint.transform.position);

            if (!eHMIActivated && selectedIndex >= 0 && distance < 25f)
            {
                if (!eHMIs[selectedIndex].activeSelf) eHMIs[selectedIndex]?.SetActive(true);
                if (selectedIndex == 4 || selectedIndex == 5 || selectedIndex == 6 || selectedIndex == 7) HandleWhomGroups(selectedIndex);
                eHMIActivated = true;
                Debug.Log($"--- eHMI activated at distance {distance:F2}m ---");
            }

            if (!isDecelerating && distance < 10f && !IsNonYieldCondition())
            {
                isDecelerating = true;
                Debug.Log($"--- Deceleration triggered at distance {distance:F2}m ---");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsNonYieldCondition() && other.gameObject == Obstacle)
        {
            isDriving = false;
            speed = 0;
        }
    }


    bool IsNonYieldCondition()
    {
        return selectedIndex == 1 || selectedIndex == 3 || selectedIndex == 7 || selectedIndex == 9 || selectedIndex == 11;
    }

    void HandleEHMIInput()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) SelectCondition(0);
        if (Input.GetKeyDown(KeyCode.Keypad2)) SelectCondition(1);
        if (Input.GetKeyDown(KeyCode.Keypad3)) SelectCondition(2);
        if (Input.GetKeyDown(KeyCode.Keypad4)) SelectCondition(3);
        if (Input.GetKeyDown(KeyCode.Keypad5)) SelectCondition(4);
        if (Input.GetKeyDown(KeyCode.Keypad6)) SelectCondition(5);
        if (Input.GetKeyDown(KeyCode.Keypad7)) SelectCondition(6);
        if (Input.GetKeyDown(KeyCode.Keypad8)) SelectCondition(7);
        if (Input.GetKeyDown(KeyCode.Keypad9)) SelectCondition(8);
        if (Input.GetKeyDown(KeyCode.Keypad0)) SelectCondition(9);
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectCondition(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectCondition(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectCondition(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectCondition(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectCondition(4);
        if (Input.GetKeyDown(KeyCode.Alpha6)) SelectCondition(5);
        if (Input.GetKeyDown(KeyCode.Alpha7)) SelectCondition(6);
        if (Input.GetKeyDown(KeyCode.Alpha8)) SelectCondition(7);
        if (Input.GetKeyDown(KeyCode.Alpha9)) SelectCondition(8);
        if (Input.GetKeyDown(KeyCode.Alpha0)) SelectCondition(9);
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) SelectCondition(10);
        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadEquals)) SelectCondition(11);
    }

    void SelectCondition(int index)
    {
        SetCondition(index);

        eHMISelected = true;

        if (conditionText != null && index >= 0 && index < eHMIConditionNames.Length)
            conditionText.text = $"Selected: {eHMIConditionNames[index]}";

        Debug.Log($"--- Key {index + 1} pressed → {eHMIConditionNames[index]} ---");
        Debug.Log("--- Now press Space to start the AV. ---");
    }

    void SetCondition(int index)
    {
        for (int i = 0; i < eHMIs.Count; i++)
            if (eHMIs[i] != null)
                eHMIs[i].SetActive(false);

        yield_ped?.SetActive(false);
        yield_dri?.SetActive(false);
        yield_cyc?.SetActive(false);
        nonyield_ped?.SetActive(false);
        nonyield_dri?.SetActive(false);
        nonyield_cyc?.SetActive(false);

        selectedIndex = index;
    }

    void HandleWhomGroups(int index)
    {
        yield_ped?.SetActive(false);
        yield_dri?.SetActive(false);
        yield_cyc?.SetActive(false);
        nonyield_ped?.SetActive(false);
        nonyield_dri?.SetActive(false);
        nonyield_cyc?.SetActive(false);

        if (index == 4)
        {
            yield_ped?.SetActive(true);
            nonyield_dri?.SetActive(true);
            nonyield_cyc?.SetActive(true);
        } else if (index == 5)
        {
            yield_cyc?.SetActive(true);
            nonyield_ped?.SetActive(true);
            nonyield_dri?.SetActive(true);
        } else if (index == 6)
        {
            yield_dri?.SetActive(true);
            nonyield_ped?.SetActive(true);
            nonyield_cyc?.SetActive(true);
        } else if (index == 7)
        {
            nonyield_ped?.SetActive(true);
            nonyield_cyc?.SetActive(true);
            nonyield_dri?.SetActive(true);
        }
    }

    void DecelerateAndStop()
    {
        if (speed > 0)
        {
            speed -= decelerationRate * Time.deltaTime;
            if (speed <= 0)
            {
                speed = 0;
                isDriving = false;
                SmoothStopAtLine();
            }
        }
    }

    void SmoothStopAtLine()
    {
        Vector3 target = TargetStopPoint.transform.position;
        float smoothTime = 0.5f;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        Debug.Log("--- AV stopped ---");
    }
}
