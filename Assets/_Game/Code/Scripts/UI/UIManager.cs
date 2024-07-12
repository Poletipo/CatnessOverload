using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{

    EventSystem _eventSystem;

    // Start is called before the first frame update
    void Awake()
    {
        _eventSystem = GetComponentInChildren<EventSystem>();
        Debug.Log(_eventSystem == null);
    }

    public EventSystem GetEventSystem(){
        return _eventSystem;
    }

    public void SetSelectedWdget(GameObject widget){
        _eventSystem.SetSelectedGameObject(widget);
    }

    public void AddWidget(GameObject widget)
    {
        widget.transform.SetParent(transform);
    }

    public GameObject GetUIWidget(Type widgetType)
    {
        return GetComponentInChildren(widgetType).gameObject;
    }

    public void ClearUI()
    {
        for (int i = 1; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
