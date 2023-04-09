using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour
{
    [SerializeField] Settings settings;
    public Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle.onValueChanged.AddListener(delegate { CamValueChanged(toggle); });
    }

    public void CamValueChanged(Toggle togglevalue)
    {
        settings.verticalCam = togglevalue.isOn;
        Debug.Log(settings.verticalCam);
    }
}