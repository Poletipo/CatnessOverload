using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject MainMenu;
    public PauseUI PauseMenu;
    public GameUI InGameMenu;



    // Start is called before the first frame update
    void Start()
    {
        PauseMenu = GetComponentInChildren<PauseUI>();
        InGameMenu = GetComponentInChildren<GameUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
