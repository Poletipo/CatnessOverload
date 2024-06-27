using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

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
