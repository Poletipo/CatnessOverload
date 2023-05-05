using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScreen : MonoBehaviour
{

    public MobileJoystick[] joysticks;


    // Start is called before the first frame update
    void Start()
    {

        if (GameManager.Instance.FirstTimeSession)
        {
            foreach (MobileJoystick item in joysticks)
            {
                item.JoystickEnabled = false;
            }

            Time.timeScale = 0;

            GameManager.Instance.FirstTimeSession = false;
        }
        else
        {
            CloseIntroScreen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CloseIntroScreen()
    {
        gameObject.SetActive(false);
        foreach (MobileJoystick item in joysticks)
        {
            item.JoystickEnabled = true;
        }
        Time.timeScale = 1;
    }


}
