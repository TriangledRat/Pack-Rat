using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneSwitcher : MonoBehaviour
{
    [SerializeField] public string sceneName;
    private GameObject clickAudio;
    private AudioSource clickAudioSource;
    public GameObject switchMenu;

    // Start is called before the first frame update
    private void Start()
    {
        clickAudio = GameObject.Find("ClickAudio");
        clickAudioSource = clickAudio.GetComponent<AudioSource>();
    }

    public void LevelSelection()
    {
        clickAudioSource.Play();
        SceneManager.LoadScene(sceneName);
    }

    public void Close()
    {
        clickAudioSource.Play();
        Application.Quit();
    }

    public void ToCredits()
    {
        clickAudioSource.Play();
        switchMenu.SetActive(true);
        GameObject.Find("MainMenu").SetActive(false);
    }

    public void ToMenu()
    {
        clickAudioSource.Play();
        var parentname = transform.parent.name;
        switchMenu.SetActive(true);
        GameObject.Find(parentname).SetActive(false);
    }
}
