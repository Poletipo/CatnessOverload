using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionUI : UIScreen
{

    [SerializeField] MapInfo MapToLoad;

    [SerializeField] TextMeshProUGUI SelectedMapText;


    // Start is called before the first frame update
    void Start()
    {
        SelectedMapText.text = MapToLoad.MapName;
    }

    public void ChangeMap(MapInfo info)
    {
        MapToLoad = info;
        SelectedMapText.text = MapToLoad.MapName;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(MapToLoad.MapNameToLoad);
    }

    public void BackToTitleScreen(){
        GameObject mainMenuUI = GameManager.Instance.UIManager.GetUIWidget(typeof(MainMenuUI));
        mainMenuUI.GetComponent<MainMenuUI>().ShowTitleScreen();
    }

    public override void Setup()
    {
        
    }
}
