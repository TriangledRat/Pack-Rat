using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField] GameObject player;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(player.transform.position);
        if (active)
        {
            player.transform.position = new Vector3(4, 12, 39);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            active = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            active = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        active = false;
    }
}
