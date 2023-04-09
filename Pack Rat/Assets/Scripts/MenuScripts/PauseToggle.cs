using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
    public bool isPaused;
    public bool continueButton;
    public GameObject pauseMenu;

    private GameObject clickAudio;
    private AudioSource clickAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        clickAudio = GameObject.Find("ClickAudio");
        clickAudioSource = clickAudio.GetComponent<AudioSource>();

    }

    public bool GetIsPaused() { return isPaused; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || continueButton)
        {
            clickAudioSource.Play();
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1.0f;
            continueButton = false;
        }

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }
}
