using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShader : MonoBehaviour
{
    Camera camera;
    [SerializeField] Shader shader;
    // Start is called before the first frame update
    void Start()
    {
        camera.GetComponent<Camera>().SetReplacementShader(shader, "Opaque");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
