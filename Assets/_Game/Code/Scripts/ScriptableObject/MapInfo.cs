using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu]
public class MapInfo : ScriptableObject
{
    public string MapName = "Map Name";
    public string MapNameToLoad = "Map Name To Load";
    public string Description = "Map Description";
    public Sprite Thumbnail;
}
