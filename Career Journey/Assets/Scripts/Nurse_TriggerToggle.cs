using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nurse_TriggerToggle : MonoBehaviour
{
    Nurse_TriggerManager triggerManager;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        triggerManager = FindObjectOfType<Nurse_TriggerManager>();
        canvas = GetComponentInChildren<Canvas>()?.gameObject;
        if (triggerManager == null)
        {
            Debug.LogWarning("Nurse_TriggerManager not found in scene.");
        }
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Use OnTriggerStay so input can be read while inside the collider
    private void OnTriggerStay(Collider other)
    {
        canvas.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (triggerManager == null) return;

            // Only advance if interacting with the active trigger (or none is active yet).
            // Compare this.gameObject (the trigger this script is attached to) with the manager's active trigger.
            var active = triggerManager.GetCurrentTrigger();
            if (active == null || this.gameObject == active)
            {
                triggerManager.AdvanceTrigger();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
    }
}

