using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColliderBehavior : MonoBehaviour
{
    [SerializeField] string textDisplay;
    [SerializeField] Text text;
    bool hit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PickUpObject") && gameObject.transform.rotation.x >= 60f || gameObject.transform.rotation.x <= -60f)
        {
            hit = true;
        }
    }

    private void Update()
    {
        if (hit && gameObject.transform.rotation.x >= 60f || gameObject.transform.rotation.x <= -60f)
        {
            text.text = textDisplay;
        }
    }

}
