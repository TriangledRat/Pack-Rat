using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineBlendDefinition;

public class ItemChecker : MonoBehaviour
{
    public bool inRange;
    public GameObject itemInRange; 
    private string labelText;
    [SerializeField] float posX, posY;
    GUIStyle style;


    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 15;
        style.normal.textColor = Color.black;
        style.alignment = TextAnchor.MiddleCenter;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickupObject" || other.tag == "HoldingObject")
        {
            inRange = true;
            itemInRange = other.gameObject;
            labelText = "Hold LeftMouse to carry";
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickupObject" || other.tag == "HoldingObject")
        {
            itemInRange = null;
            inRange = false;
            labelText = "";
        }

    }

    private void OnGUI()
    {
        Rect rect = new Rect(0, Screen.height-100, Screen.width, 120);
        GUI.Label(rect, labelText, style);

    }

}
