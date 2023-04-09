using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassThroughToSettings : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] Slider slider;
    [SerializeField] Toggle toggle;
    [SerializeField] Toggle camToggle;

    public void onReturn()
    {
        Debug.Log("Passed");
        settings.mouseSensitivity = slider.value;
        settings.tutorialOn = toggle.isOn;
        settings.verticalCam = camToggle.isOn;
    }
}
