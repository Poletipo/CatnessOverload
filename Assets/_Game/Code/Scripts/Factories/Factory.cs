using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Factory : MonoBehaviour {

    public abstract void Initialize();

    public abstract void InstantiateObject();
}
