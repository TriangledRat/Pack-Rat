using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOffTutorial : MonoBehaviour
{
    public bool tutorial;
    MainMenuSceneSwitcher MainMenuSceneSwitcher;
    public Settings settings;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuSceneSwitcher = GetComponent<MainMenuSceneSwitcher>();
        
    }

    // Update is called once per frame
    void Update()
    {
         
        if (!settings.tutorialOn)
        {
            MainMenuSceneSwitcher.sceneName = "DevScene";
        }
        
    }
}
