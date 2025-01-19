using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    string level;
    public static bool isPaused = false;

    public void Startgame()
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MENU");
        if (PC.instance != null)
            Destroy(PC.instance);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
