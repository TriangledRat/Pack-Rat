using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChecker : MonoBehaviour
{
    public bool inRange;
    public GameObject itemInRange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickupObject" || other.tag == "HoldingObject")
        {
            inRange = true;
            itemInRange = other.gameObject;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PickupObject" || other.tag == "HoldingObject")
        {
            itemInRange = null;
            inRange = false;
        }

    }


}
