using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantleScript : MonoBehaviour
{
    [HideInInspector]
    public Vector3 contactPoint;
    public float mantleSpeed;
    public LayerMask ledgeLayer;

    private bool mantling;
    private float zBound;
    private float yBound;
    private float playerLowestDistance;
    
    private Vector3 playerLowestPoint;
    private Vector3 contactNormal;
    private Quaternion rotationPoint;
    private Rigidbody playerRigidbody;
    private PlayerMovementScript moveScript;
    private MouselookScript mouselookScript;
    

    // Start is called before the first frame update
    void Start()
    {
        mantling = false;
        playerRigidbody = GetComponent<Rigidbody>();
        zBound = GetComponent<Collider>().bounds.extents.z;
        moveScript = GetComponent<PlayerMovementScript>();
        mouselookScript = GetComponent<MouselookScript>();
        playerLowestDistance = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(mantling == true)
        {
            playerRigidbody.velocity = new Vector3(0f,0f,0f);

            playerLowestPoint = transform.position;
            playerLowestPoint.y -= playerLowestDistance;

            if(Physics.Raycast(playerLowestPoint, transform.TransformDirection(Vector3.forward), zBound + 0.1f) == true)
            {
                Debug.Log("raycasting");
                transform.Translate(new Vector3(0f, mantleSpeed * Time.deltaTime,0f));
            } 
            else if(!Physics.Raycast(transform.position, -Vector3.up, playerLowestDistance + 2.0f, moveScript.GroundLayer))
            {
                transform.Translate(new Vector3(0f, 0f, mantleSpeed * Time.deltaTime));
            }
            else
            {
                //transform.Translate(new Vector3(0f, 0f, 100f * Time.deltaTime));
                mantling = false;
                moveScript.UnlockMovement();
                mouselookScript.UnlockMouseRotation();
                playerRigidbody.useGravity = true;
            }
            //rotationPoint = Quaternion.FromToRotation(transform.forward, contactNormal);
            //transform.rotation = rotationPoint;
        }
    }

    public void StartMantling()
    {
        contactNormal = transform.position - contactPoint;
        playerRigidbody.useGravity = false;
        transform.LookAt(new Vector3(contactPoint.x, transform.position.y, contactPoint.z));
        //transform.Rotate(0, contactNormal.y, 0, Space.World);
        mouselookScript.LockMouseRotation();
        mantling = true;
    }

    public bool IsMantling()
    {
        return mantling;
    }


}
