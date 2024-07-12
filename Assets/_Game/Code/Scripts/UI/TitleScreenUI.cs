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

        _mainMenuUI.GetInputs().UI.Navigate.performed += NavigationPerformed; 


        Setup();
    }

    private void NavigationPerformed(InputAction.CallbackContext context)
    {
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

    public override void Setup()
    {
        Debug.Log("Seeteup title");
        GameManager.Instance.UIManager.SetSelectedWdget(_firstWidget);
    }

    //TODO : Quit Button
}
