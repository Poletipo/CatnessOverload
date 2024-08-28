using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{

    [SerializeField] Animation _animation;

    bool _isTransitioning = false;


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void StartScreenTransition(){
        _isTransitioning = true;
        _animation.Play("ScreenTransitStart");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(!_isTransitioning){
            return;
        }

        _isTransitioning = false;

        _animation.Play("ScreenTransitEnd");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
