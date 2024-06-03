using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIscript : MonoBehaviour
{
    public void mainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void playGame()
    {
        SceneManager.LoadScene(4);
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);
    }

    public void Settings()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void backSetting()
    {
        SceneManager.LoadScene(1);
    }

    public void backCredits()
    {
        SceneManager.LoadScene(1);
    }
}
