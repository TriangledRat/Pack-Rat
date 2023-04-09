using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbObjectHighlighter : MonoBehaviour
{
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = this.GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", Color.white);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            material.DisableKeyword("_EMISSION");
        }
    }
}
