using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionUI : UIScreen
{

    [SerializeField] MapInfo MapToLoad;

    [SerializeField] TextMeshProUGUI SelectedMapText;
    [SerializeField] Image SelectedMapImage;

    [SerializeField] GameObject _firstWidget;
    [SerializeField] GameObject _playButton;

    Button button;


    // Start is called before the first frame update
    void Start()
    {
        ChangeMap(MapToLoad);
        GameManager.Instance.UIManager.GetInputs().UI.Cancel.performed += BackToTitleScreenInput;
        GameManager.Instance.UIManager.GetInputs().UI.Navigate.performed += NavigationPerformed;
    }

    public void SelectMap()
    {
        GameManager.Instance.UIManager.GetEventSystem().SetSelectedGameObject(_playButton);
    }

    void OnDestroy (){
        GameManager.Instance.UIManager.GetInputs().UI.Cancel.performed -= BackToTitleScreenInput;
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



    private void BackToTitleScreenInput(InputAction.CallbackContext context)
    {
        if(GameManager.Instance.UIManager.GetEventSystem().currentSelectedGameObject == _playButton){
            GameManager.Instance.UIManager.SetSelectedWdget(_firstWidget);
        }
        else{
            BackToTitleScreen();
        }

    }

    public void ChangeMap(MapInfo info)
    {
        MapToLoad = info;
        SelectedMapText.text = MapToLoad.MapName;
        SelectedMapImage.sprite = MapToLoad.Thumbnail;
    }

    public async void LoadLevel()
    {
        GameManager.Instance.UIManager.StartScreenTransition();
        await Task.Delay(500);
        SceneManager.LoadScene(MapToLoad.MapNameToLoad);
    }

    public void BackToTitleScreen(){
        GameObject mainMenuUI = GameManager.Instance.UIManager.GetUIWidget(typeof(MainMenuUI));
        mainMenuUI.GetComponent<MainMenuUI>().ShowTitleScreen();
    }

    public override void Setup()
    {
        GameManager.Instance.UIManager.SetSelectedWdget(_firstWidget);
    }
}
