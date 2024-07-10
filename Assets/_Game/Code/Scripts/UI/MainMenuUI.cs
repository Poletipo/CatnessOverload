using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] LevelSelectionUI _levelSelectionScreen;
    [SerializeField] GameObject _titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UIManager.ClearUI();
        GameManager.Instance.UIManager.AddWidget(gameObject);
    }

    public void ShowLevelSelectionScreen(){
        _titleScreen.SetActive(false);
        _levelSelectionScreen.gameObject.SetActive(true);
    }

    public void ShowTitleScreen(){
        _titleScreen.SetActive(true);
        _levelSelectionScreen.gameObject.SetActive(false);
    }


}
