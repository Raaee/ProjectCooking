using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Workstation")]
public class WorkstationSO : ScriptableObject {

    public GameObject prefab;
    public Sprite normalSprite;
    public Sprite highlightedSprite;
    public string displayName;
}
