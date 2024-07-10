using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowLevelSelectionScreen(){
        GameObject mainMenu = GameManager.Instance.UIManager.GetUIWidget(typeof(MainMenuUI));
        mainMenu.GetComponent<MainMenuUI>().ShowLevelSelectionScreen();
    }

    //TODO : Go to level selection
    //TODO : Quit Button
}
