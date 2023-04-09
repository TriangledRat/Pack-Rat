using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] bool useOffset;
    [SerializeField] Settings settings;
    public Transform target;
    public Vector3 offset;
    public float rotationSpeed;
    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public float rotationModifier;
    public bool invertVerticalCam;

    // Start is called before the first frame update
    void Start()
    {               
        if (!useOffset)
        {
            offset = target.position - transform.position;            
        }
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        //enabled this when building!
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        rotationModifier = settings.mouseSensitivity;
        invertVerticalCam = settings.verticalCam;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        if(Time.timeScale > 0)
        {
            MoveWithMouse();
            transform.LookAt(target);
        }
        
    }

    void MoveWithMouse()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * rotationModifier;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed * rotationModifier;
        target.Rotate(0, horizontal, 0);
        if (!invertVerticalCam)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        //if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.y < 360 + minViewAngle)
        //{
        //    pivot.rotation = Quaternion.Euler(360 + minViewAngle, 0, 0);
        //}

        float targetYAngle = target.eulerAngles.y;
        float targetXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(targetXAngle, targetYAngle, 0);
        transform.position = target.position - (rotation * offset);
    }
}
