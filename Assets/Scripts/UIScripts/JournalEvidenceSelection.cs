using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class JournalEvidenceSelection : MonoBehaviour
{
    public GameObject selected;
    public GameObject ruleOutEv;
    public GhostEvidence ghostEvidence;
    public TMP_Dropdown dropDown;

    public enum SelectionState { NONE, SELECTED, RULED_OUT };
    private SelectionState selectionState { get; set; }

    void Start()
    {
        List<GhostType> ghostTypeList = Enum.GetValues(typeof(GhostType)).Cast<GhostType>().ToList();
        PopulateTheDropDownList(ghostTypeList);
    }
    
    void Update()
    {
    }

    public void RecordEvidenceOnClick()
    {
        if (!selected.activeInHierarchy && !ruleOutEv.activeInHierarchy)
        {
            selected.SetActive(true);
            ruleOutEv.SetActive(false);
            selectionState = SelectionState.SELECTED;
        }
        else if (selected.activeInHierarchy)
        {
            selected.SetActive(false);
            ruleOutEv.SetActive(true);
            selectionState = SelectionState.RULED_OUT;
        }
        else if (ruleOutEv.activeInHierarchy)
        {
            ruleOutEv.SetActive(false);
            selected.SetActive(false);
            selectionState = SelectionState.NONE;
        }

        UpdateDropDownList();
    }

    private void PopulateTheDropDownList(List<GhostType> ghostTypeList)
    {
        dropDown.ClearOptions();
        // Create a new empty options list
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData("Unknown"));

        foreach (GhostType ghostType in ghostTypeList)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(ghostType.ToString());
            options.Add(option);
        }

        // Add the options to the dropdown
        dropDown.AddOptions(options);
    }

    private void UpdateDropDownList()
    {
        List<GhostType> ghostTypeList = Enum.GetValues(typeof(GhostType)).Cast<GhostType>().ToList();

        // Get all evidence objects
        JournalEvidenceSelection[] journalEvidenceSelectionObjects = FindObjectsOfType<JournalEvidenceSelection>();

        foreach (JournalEvidenceSelection jesObj in journalEvidenceSelectionObjects)
        {
            ghostTypeList = FilterOptionsBySelection(ghostTypeList, jesObj);
        }

        PopulateTheDropDownList(ghostTypeList);
    }

    private List<GhostType> FilterOptionsBySelection(List<GhostType> ghostTypeList, JournalEvidenceSelection jesObj)
    {
        List<GhostType> filteredGhostTypeList = new List<GhostType>();

        foreach (GhostType ghostType in ghostTypeList)
        {
            if (IsGhostTypeIncluded(ghostType, jesObj))
            {
                filteredGhostTypeList.Add(ghostType);
            }
        }

        return filteredGhostTypeList;
    }

    private bool IsGhostTypeIncluded(GhostType ghostType, JournalEvidenceSelection jesObj)
    {
        // Don't filter the list when no evidence is selected
        if (jesObj.selectionState == SelectionState.NONE)
        {
            return true;
        }

        GhostEvidence[] ghostEvidences = Ghost.ghostTypeEvidenceArray[(int)ghostType];

        // Included the ghosts with the selected evidence
        if (jesObj.selectionState == SelectionState.SELECTED)
        {
            foreach (GhostEvidence ge in ghostEvidences)
            {
                if (ge == jesObj.ghostEvidence)
                {
                    return true;
                }
            }

            return false;
        }
        // Exclude the ghosts with the ruled out evidence
        else if (jesObj.selectionState == SelectionState.RULED_OUT)
        {
            foreach (GhostEvidence ge in ghostEvidences)
            {
                if (ge == jesObj.ghostEvidence)
                {
                    return false;
                }
            }

            return true;
        }

        // Any ghosts not filtered by the above rules will be included by default.
        return true;
    }
}
