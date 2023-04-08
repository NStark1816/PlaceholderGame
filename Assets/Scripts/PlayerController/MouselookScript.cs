using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouselookScript : MonoBehaviour
{
    public float mouseSensitivity;

    //Rigidbody PlayerRigidbody;
    //Vector3 EulerAngleVelocity;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * mouseSensitivity);
        //EulerAngleVelocity = new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
        //Quaternion deltaRotation = Quaternion.Euler(EulerAngleVelocity * Time.deltaTime);
        //PlayerRigidbody.MoveRotation(PlayerRigidbody.rotation * deltaRotation);

    }
}
