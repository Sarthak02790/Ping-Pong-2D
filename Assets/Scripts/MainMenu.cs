using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayervsPlayer()
    {
        SceneManager.LoadScene("PlayerScene");
    }

    public void AiPlayer()
    {
        SceneManager.LoadScene("AIPlayer");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
