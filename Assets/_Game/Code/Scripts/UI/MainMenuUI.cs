using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] LevelSelectionUI _levelSelectionScreen;
    [SerializeField] TitleScreenUI _titleScreen;
    

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.UIManager.ClearUI();
        GameManager.Instance.UIManager.AddWidget(gameObject);
        
    }

    public void ShowLevelSelectionScreen(){
        _titleScreen.gameObject.SetActive(false);
        _levelSelectionScreen.gameObject.SetActive(true);
        _levelSelectionScreen.Setup();
    }

    public void ShowTitleScreen(){
        _titleScreen.gameObject.SetActive(true);
        _levelSelectionScreen.gameObject.SetActive(false);
        _titleScreen.Setup();
    }


}
