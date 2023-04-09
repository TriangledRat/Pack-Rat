using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class TriggerBehavior : MonoBehaviour
{
    [SerializeField] string textDisplay;
    [SerializeField] Text text;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            text.text = textDisplay;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && this.gameObject.name == "Trigger 1")
        {
            text.text = textDisplay;
        }
    }

}
