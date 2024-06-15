using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChager : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    // Update is called once per frame
    void Update()
    {
       
    }

    public void fadeToNextScene()
    {

    }

    public void FadeToLevel (int LevelIndex)
    {
        levelToLoad = LevelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void Play()
    {
        FadeToLevel(5);
        animator.SetTrigger("FadeOut");
    }

    public void mainMenu()
    {
        FadeToLevel(1);
        animator.SetTrigger("FadeOut");
    }

    public void Settings()
    {
        FadeToLevel(3);
        animator.SetTrigger("FadeOut");
    }

    public void Credits()
    {
        FadeToLevel(2);
        animator.SetTrigger("FadeOut");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void backSetting()
    {
        FadeToLevel(1);
        animator.SetTrigger("FadeOut");
    }

    public void backCredits()
    {
        FadeToLevel(1);
        animator.SetTrigger("FadeOut");
    }
}
