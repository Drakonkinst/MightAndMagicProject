using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;          // The target to follow
    public float playerDistance;      // The distance from the target
    public float height;              // The height of the camera above the target

    private Transform myTransform;

    void Start()
    {
        if(target == null) {
            Debug.LogWarning("No target for camera found!");
        }

        // cache transform
        myTransform = transform;
        UpdateIsometric();
    }

    void Update()
    {
        // Nothing here
    }

    void LateUpdate() {
        UpdateIsometric();
        
    }

    private void UpdateIsometric() {
        myTransform.position = target.position + new Vector3(0, height, -playerDistance);
        myTransform.LookAt(target.position);

    }
}
