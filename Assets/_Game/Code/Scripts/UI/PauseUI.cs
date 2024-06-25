using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{

    public GameObject PauseMenuOrigin;

    // Start is called before the first frame update
    void Start()
    {
        PauseManager.OnPause += OpenPauseMenu;
        PauseManager.OnUnPause += ClosePauseMenu;
    }

    public void OpenPauseMenu()
    {
        PauseMenuOrigin.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        PauseMenuOrigin.SetActive(false);
    }

    public void ResumeGame()
    {
        PauseManager.UnPauseGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
