using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UIManager.ClearUI();
        GameManager.Instance.UIManager.AddWidget(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
