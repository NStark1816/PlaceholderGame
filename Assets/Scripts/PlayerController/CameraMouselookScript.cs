using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouselookScript : MonoBehaviour
{
    public float mouseSensitivity;

    public float inputXAxis;

    // Update is called once per frame
    void Update()
    {
        inputXAxis = Input.GetAxis("Mouse Y") * -1;

        //if (inputXAxis )
            transform.Rotate((new Vector3((inputXAxis), 0, 0)) * Time.deltaTime * mouseSensitivity);
    }
}
