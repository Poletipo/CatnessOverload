using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{

    EventSystem _eventSystem;

    private CatnessOverloadInputs _inputs;

    [SerializeField] Transform _permanentUI;
    [SerializeField] Transform _TempUI;

    public CatnessOverloadInputs GetInputs(){
        return _inputs;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _eventSystem = GetComponentInChildren<EventSystem>();
        _inputs = new CatnessOverloadInputs();
        _inputs.Enable();
    }

    public EventSystem GetEventSystem(){
        return _eventSystem;
    }

    public void SetSelectedWdget(GameObject widget){
        _eventSystem.SetSelectedGameObject(widget);
    }

    public void AddWidget(GameObject widget)
    {
        widget.transform.SetParent(_TempUI);
    }

    public GameObject GetUIWidget(Type widgetType)
    {

        if(_TempUI.GetComponentInChildren(widgetType) == null){
            return null;
        }

        return _TempUI.GetComponentInChildren(widgetType).gameObject;
    }

    public void ClearUI()
    {
        for (int i = 0; i < _TempUI.childCount; i++) {
            Destroy(_TempUI.GetChild(i).gameObject);
        }
    }

    public void StartScreenTransition(){
        _permanentUI.GetComponentInChildren<ScreenTransition>().StartScreenTransition();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
