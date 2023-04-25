using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantleCheckScript : MonoBehaviour
{
    
    private GameObject playerObject;
    private PlayerMovementScript moveScript;
    private MantleScript mantleScript;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        moveScript = playerObject.GetComponent<PlayerMovementScript>();
        mantleScript = playerObject.GetComponent<MantleScript>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if(mantleScript.IsMantling() == false)
        {
            if(coll.tag == "Ledge")
            {
                moveScript.LockMovement();
                mantleScript.contactPoint = coll.ClosestPoint(playerObject.transform.position);
                mantleScript.StartMantling();
            }
        }
    }
}
