using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour {

    public GameObject PauseMenuOrigin;
    private GameUI _gameUI;

    // Start is called before the first frame update
    void Start()
    {
        _gameUI = GameManager.Instance.GameUI;
    }

    public void OpenPauseMenu()
    {
        PauseMenuOrigin.SetActive(true);
        _gameUI.HideUI();
    }

    public void ClosePauseMenu()
    {
        PauseMenuOrigin.SetActive(false);
        _gameUI.ShowUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
