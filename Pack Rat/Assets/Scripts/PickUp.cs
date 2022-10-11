using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    GameObject pickedUpObject;
    GameObject heldObject;
    bool pickup;
    public Transform theDestination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if (pickup && pickedUpObject != null && Input.GetMouseButton(0))
        {
            Destroy(pickedUpObject);
            Instantiate(heldObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickupObject")
        {
            pickup = true;
            pickedUpObject = other.gameObject;
        }
    }
}
