using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSettingsReader : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = settings.mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
