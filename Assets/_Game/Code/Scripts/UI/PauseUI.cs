using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseUI : UIScreen
{

    public GameObject PauseMenuOrigin;

    [SerializeField] GameObject _firstWidget;

    // Start is called before the first frame update
    void Start()
    {

        GameManager.Instance.UIManager.GetInputs().UI.Navigate.performed += NavigationPerformed;
        GameManager.Instance.UIManager.GetInputs().UI.Cancel.performed += UnPause;

        PauseManager.OnPause += OpenPauseMenu;
        PauseManager.OnUnPause += ClosePauseMenu;
    }

    private void UnPause(InputAction.CallbackContext context)
    {
        if(!PauseManager.GetPauseStatus()){
            return;
        }

        PauseManager.UnPauseGame();
    }

    private void NavigationPerformed(InputAction.CallbackContext context)
    {
        
        if(!PauseMenuOrigin.activeSelf){
            return;
        }

        if(GameManager.Instance.UIManager.GetEventSystem().currentSelectedGameObject != null ){
            return;
        }

        GameManager.Instance.UIManager.SetSelectedWdget(_firstWidget);
    }

    private void OnDestroy()
    {
        PauseManager.OnPause -= OpenPauseMenu;
        PauseManager.OnUnPause -= ClosePauseMenu;

        GameManager.Instance.UIManager.GetInputs().UI.Navigate.performed -= NavigationPerformed;
        GameManager.Instance.UIManager.GetInputs().UI.Cancel.performed -= UnPause;
    }

    public void OpenPauseMenu()
    {
        PauseMenuOrigin.SetActive(true);
        Setup();
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
        PauseManager.UnPauseGame();
        SceneManager.LoadScene(0);
    }

    public override void Setup()
    {
        GameManager.Instance.UIManager.GetEventSystem().SetSelectedGameObject(_firstWidget);
    }


    void Update(){
    }

}
