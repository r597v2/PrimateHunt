using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankIsSelected : MonoBehaviour
{
    public GameObject bankSelection;
    public static bool bankIsSelected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void BankSelected()
    {
        bankSelection.SetActive(true);
        bankIsSelected = true;
    }
}
