using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompletionChecker : MonoBehaviour
{
    private List<GameObject> completionList = new List<GameObject>();
    public int objects = 1;

    // Update is called once per frame
    void Update()
    {
        if (completionList.Count == objects)
        {
            SceneManager.LoadScene("Testscene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickupObject")
        {
            completionList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickupObject")
        {
            completionList.Remove(other.gameObject);
        }

    }
}
