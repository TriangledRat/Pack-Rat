using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static Cinemachine.CinemachineBlendDefinition;

public class CompletionChecker : MonoBehaviour
{
    private List<GameObject> completionList = new List<GameObject>();
    public int objects = 6;
    int listedObjects;
    private string labelText;
    [SerializeField] float posX, posY;
    GUIStyle style;

    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 15;
        style.normal.textColor = Color.black;
        style.alignment = TextAnchor.MiddleCenter;
        listedObjects = objects;
    }
    // Update is called once per frame
    void Update()
    {
        labelText = "Items remaining: " + listedObjects;
        if (completionList.Count == objects)
        {
            SceneManager.LoadScene("CompleteScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickupObject")
        {
            completionList.Add(other.gameObject);
            listedObjects--;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickupObject")
        {
            completionList.Remove(other.gameObject);
            listedObjects++;
        }

    }

    private void OnGUI()
    {
        Rect rect = new Rect(-Screen.width/2+300, posY, Screen.width, 120);
        GUI.Label(rect, labelText, style);

    }
}
