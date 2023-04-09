using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleSettingsReader : MonoBehaviour
{
    [SerializeField] Settings settings;
    [SerializeField] Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = settings.tutorialOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
