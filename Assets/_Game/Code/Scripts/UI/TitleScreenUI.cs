using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TitleScreenUI : UIScreen
{

    [SerializeField] GameObject _firstWidget;
    MainMenuUI _mainMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuUI = GameManager.Instance.UIManager.
            GetUIWidget(typeof(MainMenuUI)).GetComponent<MainMenuUI>();

        GameManager.Instance.UIManager.GetInputs().UI.Navigate.performed += NavigationPerformed; 


        Setup();
    }

    void OnDestroy(){
        GameManager.Instance.UIManager.GetInputs().UI.Navigate.performed -= NavigationPerformed;
    }

    private void NavigationPerformed(InputAction.CallbackContext context)
    {
        if(!isActiveAndEnabled){
            return;
        }

        if(GameManager.Instance.UIManager.GetEventSystem().currentSelectedGameObject != null ){
            return;
        }

        GameManager.Instance.UIManager.SetSelectedWdget(_firstWidget);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelSelectionScreen(){
        _mainMenuUI.ShowLevelSelectionScreen();
    }

    public void QuitGame(){
        Application.Quit();
    }

    public override void Setup()
    {
        GameManager.Instance.UIManager.SetSelectedWdget(_firstWidget);
    }
}
