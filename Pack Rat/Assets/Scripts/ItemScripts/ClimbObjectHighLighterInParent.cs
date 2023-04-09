using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbObjectHighLighterInParent : MonoBehaviour
{
    Renderer[] renderers;   
    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach(Renderer rend in renderers)
            {
                foreach(Material mat in rend.materials)
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", Color.white);
                }
                
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (Renderer rend in renderers)
            {
                foreach (Material mat in rend.materials)
                {
                    mat.DisableKeyword("_EMISSION");

                }
            }
        }
    }
}
