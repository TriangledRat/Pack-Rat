using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScripts : MonoBehaviour
{
    [SerializeField] GameObject pauseHandler;
    PauseToggle pausetoggle;
    private GameObject clickAudio;
    private AudioSource clickAudioSource;
    // Start is called before the first frame update

    private void Start()
    {
        pausetoggle = pauseHandler.GetComponent<PauseToggle>();
        clickAudio = GameObject.Find("ClickAudio");
        clickAudioSource = clickAudio.GetComponent<AudioSource>();
    }
    public void UnPause()
    {
        clickAudioSource.Play();
        pausetoggle.continueButton = true;
    }

    public void ToMainMenu()
    {
        clickAudioSource.Play();
        pausetoggle.continueButton = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void CloseGame()
    {
        clickAudioSource.Play();
        pausetoggle.continueButton = true;
        Application.Quit();
    }
 
}
