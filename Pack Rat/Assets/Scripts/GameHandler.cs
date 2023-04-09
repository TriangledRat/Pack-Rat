using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject pickupList;
    public List<GameObject> gameList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        AddToList();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddToList()
    {
        foreach(Transform tr in pickupList.gameObject.GetComponentsInChildren<Transform>())
        {
            gameList.Add(tr.gameObject);
        }
    }
}
