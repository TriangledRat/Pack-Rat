using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] Settings settings;
    public Toggle toggle, otherToggle;
    public Slider slider;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameObject.Find("Credits").SetActive(false);
        toggle.onValueChanged.AddListener(delegate { TutorialValueChanged(toggle); });
        otherToggle.onValueChanged.AddListener(delegate { CamValueChanged(otherToggle); });
    }

    public void TutorialValueChanged(Toggle togglevalue)
    {
        settings.tutorialOn = togglevalue.isOn;
    }

    public void CamValueChanged(Toggle togglevalue)
    {
        settings.verticalCam = togglevalue.isOn;
    }

    public void SliderValue(Slider slidervalue)
    {
        settings.mouseSensitivity = slidervalue.value;
    }

    private void Update()
    {
        Debug.Log(settings.tutorialOn);
    }



}
