using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Settings", menuName = "Assets/Michael/Assets/Prefab")]
public class Settings : ScriptableObject
{
    public float mouseSensitivity = 1;
    public bool tutorialOn = true;
    public bool verticalCam = false;

}
