using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float walkDistance;
    public float runDistance;
    public float height;

    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null) {
            Debug.LogWarning("No target for camera found!");
        }

        // cache transform
        myTransform = transform;
        UpdateIsometric();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate() {
        UpdateIsometric();
        
    }

    private void UpdateIsometric() {
        myTransform.position = target.position + new Vector3(0, height, -walkDistance);
        myTransform.LookAt(target.position);

    }

    /*
        playerMoveDir = player.transform.position - playerPrevPos;
    if (playerMoveDir != Vector3.zero)
    {
        playerMoveDir.normalize();
        transform.position = player.transform.position - playerMoveDir * distance;

        transform.position.y += 5f; // required height

        transform.LookAt(player.transform.position);

        playerPrevPos = player.transform.position;
    }*/
    /*
        Vector3 playerMoveDir = target.position - playerPrevPos;
        if(playerMoveDir != Vector3.zero) {
            playerMoveDir.Normalize();
            
            //myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
            myTransform.position = target.position - playerMoveDir * walkDistance;
            myTransform.position += new Vector3(0, height, 0);
            myTransform.LookAt(target.position);
            playerPrevPos = target.position;
        }
        
        */
}
