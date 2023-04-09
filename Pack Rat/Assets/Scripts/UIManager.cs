using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI toDoList;
    [SerializeField] GameObject manager;
    public string element1, element2, element3, element4, element5, element6, element7;

    // Start is called before the first frame update
    void Start()
    {
        element1 = "Pickup Item 1";
        element2 = "Pickup Item 2";
        element3 = "Pickup Item 3";
        element4 = "Pickup Item 4";
        element5 = "Pickup Item 5";
        element6 = "Pickup Item 6";
        element7 = "Pickup Item 7";

    }

    // Update is called once per frame
    void Update()
    {
        toDoList.text = element1 + "\n" + element2 + "\n" + element3 + "\n" + element4 + "\n" + element5 + "\n" + element6 + "\n" + element7;      
    }


}
