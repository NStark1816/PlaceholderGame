using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouselookScript : MonoBehaviour
{
    public float mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((new Vector3((Input.GetAxis("Mouse Y") * -1), 0, 0)) * Time.deltaTime * mouseSensitivity);
    }
}
