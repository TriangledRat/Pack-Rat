using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPlayerTrigger : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Camera cam;
    MovementController controller;
    CameraController cameraController;
    int frameDelay;
    int textCounter;
    void Start()
    {
        cameraController = cam.GetComponent<CameraController>();
        textCounter = 0;
        controller = GetComponent<MovementController>();
        controller.enabled = false;
        cameraController.enabled = false;
        frameDelay = 3;

    }

    // Update is called once per frame
    void Update()
    {
        TextChanger();
        if (frameDelay > 0)
        {
            frameDelay--;
        }

        if(frameDelay == 0 && Input.GetKeyDown(KeyCode.E) && textCounter <2)
        {
            textCounter++;
            frameDelay = 3;
        }

        if (transform.position.z > -7.7f)
        {
            text.text = "";
           enabled = false;
        }
        
    }

    void TextChanger()
    {
        switch (textCounter)
        {
            case 0:
                text.text = "You hear that? The family is awake and getting ready for a work day. They're a chaotic bunch, so they'll forget half of the things they need... \n Thankfully, you know this house as well as your own tail, so you best go and help them gather their stuff. " +
                    "\nPress E to continue...";
                break;
            case 1:
                cameraController.enabled = true;
                text.text = "You slept pretty deep though, so let's do some exercises to wake up right. You can control the camera with your mouse, and you'll automatically turn in that direction." + "\nPress E again when you're ready.";
                break;
            case 2:
                controller.enabled = true;
                text.text = "You move around with the WASD keys, and jump with the spacebar. Get schmovin'! When you're ready, head on through the gap.";
                break;
            default:
                text.text = "";
                break;


        }
    }
}
