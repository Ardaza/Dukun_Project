using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player; // Reference to the player GameObject
    public GameObject[] additionalObjects; // References to additional GameObjects to be paused
    public static bool isPaused;

    private List<Behaviour> scriptsToPause = new List<Behaviour>(); // List of scripts to pause

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        FindScriptsToPause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Find all active scripts on objects under the parent GameObject of the player
    void FindScriptsToPause()
    {
        if (player != null)
        {
            Transform parent = player.transform.parent; // Get the parent GameObject of the player
            if (parent != null)
            {
                // Get all active scripts under the parent GameObject and store them in the list
                Behaviour[] behaviours = parent.GetComponentsInChildren<Behaviour>(true);
                foreach (Behaviour behaviour in behaviours)
                {
                    if (behaviour.enabled)
                    {
                        scriptsToPause.Add(behaviour);
                    }
                }
            }
        }

        // Add scripts of additional GameObjects to the list
        foreach (GameObject obj in additionalObjects)
        {
            if (obj != null)
            {
                Behaviour[] behaviours = obj.GetComponentsInChildren<Behaviour>(true);
                foreach (Behaviour behaviour in behaviours)
                {
                    if (behaviour.enabled)
                    {
                        scriptsToPause.Add(behaviour);
                    }
                }
            }
        }
    }

    // Pause the game
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disable all active scripts when the game is paused
        foreach (Behaviour behaviour in scriptsToPause)
        {
            behaviour.enabled = false;
        }
    }

    // Resume the game
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        // Enable all scripts that were disabled when the game was paused
        foreach (Behaviour behaviour in scriptsToPause)
        {
            behaviour.enabled = true;
        }
    }

    // Go to the main menu
    public void Settings()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(3);
    }

    // Quit the application
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
