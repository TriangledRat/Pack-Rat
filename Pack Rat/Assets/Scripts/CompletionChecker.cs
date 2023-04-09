using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class CompletionChecker : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject completionAudio, leavingAudio;
    AudioSource completionAudioSource, leavingAudioSource;
    UIManager uiManager;
    public int amountofObjects;
    int counter;


    void Start()
    {
        completionAudioSource = completionAudio.GetComponent<AudioSource>();
        leavingAudioSource = leavingAudio.GetComponent<AudioSource>();
        uiManager = canvas.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == amountofObjects)
        {
            SceneManager.LoadScene("CompleteScene");
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "Pickup Item 1")
        {
            completionAudioSource.Play();
            uiManager.element1 = "<s>Pickup Item 1</s>";
            counter++;
        }

        if (other.name == "Pickup Item 2")
        {
            completionAudioSource.Play();
            uiManager.element2 = "<s>Pickup Item 2</s>";
            counter++;
        }

        if (other.name == "Pickup Item 3")
        {
            completionAudioSource.Play();
            uiManager.element3 = "<s>Pickup Item 3</s>";
            counter++;
        }

        if (other.name == "Pickup Item 4")
        {
            completionAudioSource.Play();
            uiManager.element4 = "<s>Pickup Item 4</s>";
            counter++;
        }


        if (other.name == "Pickup Item 5")
        {
            completionAudioSource.Play();
            uiManager.element5 = "<s>Pickup Item 5</s>";
            counter++;
        }

        if (other.name == "Pickup Item 6")
        {
            completionAudioSource.Play();
            uiManager.element6 = "<s>Pickup Item 6</s>";
            counter++;
        }
        if (other.name == "Pickup Item 7")
        {
            completionAudioSource.Play();
            uiManager.element7 = "<s>Pickup Item 7</s>";
            counter++;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.name == "Pickup Item 1")
        {
            leavingAudioSource.Play();
            uiManager.element1 = "Pickup Item 1";
            counter--;
        }

        if (other.name == "Pickup Item 2")
        {
            leavingAudioSource.Play();
            uiManager.element2 = "Pickup Item 2";
            counter--;
        }

        if (other.name == "Pickup Item 3")
        {
            leavingAudioSource.Play();
            uiManager.element3 = "Pickup Item 3";
            counter--;
        }

        if (other.name == "Pickup Item 4")
        {
            leavingAudioSource.Play();
            uiManager.element4 = "Pickup Item 4";
            counter--;
        }


        if (other.name == "Pickup Item 5")
        {
            leavingAudioSource.Play();
            uiManager.element5 = "Pickup Item 5";
            counter--;
        }

        if (other.name == "Pickup Item 6")
        {
            leavingAudioSource.Play();
            uiManager.element6 = "Pickup Item 6";
            counter--;
        }
        if (other.name == "Pickup Item 7")
        {
            leavingAudioSource.Play();
            uiManager.element7 = "Pickup Item 7";
            counter--;
        }


    }

}
