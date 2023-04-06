using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LaptopGhostSelect : MonoBehaviour
{
    public GameObject[] ghosts = new GameObject[]{};

    public void Select(int ghost)
    {
        foreach(GameObject g in ghosts)
        {
            g.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        ghosts[ghost].GetComponent<Image>().color = new Color32(69, 255, 49, 255);
    }
}
