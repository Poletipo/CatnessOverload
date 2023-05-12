using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

    public static Action OnPause;
    public static Action OnUnPause;


    public static bool gameIsPaused;


    public static void PauseGame()
    {
        if (gameIsPaused) {
            OnPause?.Invoke();
            Time.timeScale = 1;
            gameIsPaused = false;
        }
        else {
            OnUnPause?.Invoke();
            Time.timeScale = 0;
            gameIsPaused = true;
        }

    }

}
