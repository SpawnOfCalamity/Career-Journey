using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Nurse_TriggerManager : MonoBehaviour
{
    public bool login = true;
    public GameObject loginTrigger;
    public bool BP = false;
    public GameObject BPTrigger;
    public bool patient = false;
    public GameObject patientTrigger;
    public bool paper = false;
    public GameObject paperTrigger;

    public List<GameObject> triggers;

    // runtime index of the currently active trigger (-1 = none)
    public int currentIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        // ensure list exists and - if empty - populate from explicit fields for backward compatibility
        if (triggers == null) triggers = new List<GameObject>();
        if (triggers.Count == 0)
        {
            if (loginTrigger) triggers.Add(loginTrigger);
            if (BPTrigger) triggers.Add(BPTrigger);
            if (patientTrigger) triggers.Add(patientTrigger);
            if (paperTrigger) triggers.Add(paperTrigger);
        }

        // deactivate any list entries at start
        DeactivateAll();
        currentIndex = -1;

        // If loginTrigger should start active, ensure manager's currentIndex matches that trigger
        if (loginTrigger)
        {
            int idx = triggers.IndexOf(loginTrigger);
            if (idx < 0)
            {
                // not present in list — add it so manager can control it
                idx = triggers.Count;
                triggers.Add(loginTrigger);
            }
            currentIndex = idx;
            if (currentIndex >= 0 && currentIndex < triggers.Count)
            {
                triggers[currentIndex].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Deactivate all triggers in the list
    public void DeactivateAll()
    {
        if (triggers == null) return;
        foreach (var t in triggers)
        {
            if (t) t.SetActive(false);
        }
    }

    // Returns the currently active trigger or null
    public GameObject GetCurrentTrigger()
    {
        if (triggers == null) return null;
        if (currentIndex >= 0 && currentIndex < triggers.Count) return triggers[currentIndex];
        return null;
    }

    // Advance to the next trigger: deactivate current, activate next.
    // When the end is reached the list is fully deactivated and index resets to -1.
    public void AdvanceTrigger()
    {
        Debug.Log("Advancing trigger...");
        if (triggers == null || triggers.Count == 0) return;

        // deactivate current
        if (currentIndex >= 0 && currentIndex < triggers.Count)
        {
            var cur = triggers[currentIndex];
            if (cur) cur.SetActive(false);
        }

        int nextIndex = currentIndex + 1;
        if (nextIndex < triggers.Count)
        {
            currentIndex = nextIndex;
            var next = triggers[currentIndex];
            if (next) next.SetActive(true);
        }
        else
        {
            // reached end: turn everything off and reset index
            DeactivateAll();
            currentIndex = -1;
        }
    }
}
