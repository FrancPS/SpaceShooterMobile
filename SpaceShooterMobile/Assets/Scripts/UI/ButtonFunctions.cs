using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public AudioSource buttonClicked;
    public AudioSource openMenu;
    public AudioSource closeMenu;

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Using buildIndex is faster than comparing names
        PauseGame(false);
    }

    public void PauseGame(bool pause)
    {
       if (pause) { Time.timeScale = 0; }
       else { Time.timeScale = 1; }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Only applicable because we only have 1 game scene
        PauseGame(false);
    }

    public void PlayButtonClickedAudio()
    {
        buttonClicked.Play();
    }

    public void PlayOpenMenuAudio()
    {
        openMenu.Play();
    }

    public void PlayCloseMenuAudio()
    {
        closeMenu.Play();
    }
}
