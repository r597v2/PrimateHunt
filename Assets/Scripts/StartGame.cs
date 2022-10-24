using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void GameStart()
    {
        if (BankIsSelected.bankIsSelected)
        {
            OpenBankScene();
        }
    }

    public void OpenBankScene()
    {
        SceneManager.LoadScene("Bank");

        SceneManager.UnloadSceneAsync("Lobby");
    }
}
