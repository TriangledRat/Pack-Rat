using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class WindowQuestPointer : MonoBehaviour
{

    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    [SerializeField] Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector3(-13, 56, 89);
        pointerRectTransform = transform.Find("Arrow").GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = cam.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0f, angle);
        
    }
}
