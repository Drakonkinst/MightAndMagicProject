using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorld : MonoBehaviour
{
    public float moveSpeed = 5F; // units/sec
    public float rotationSpeed = 1000F;
    public Transform anchor;
    
    private Vector3 moveDirection;
    private Transform myTransform;

    void Start() {
        myTransform = transform;
    }

    void Update() {
        GetInput();
        MoveCharacter();
    }

    private void GetInput() {
        moveDirection = Vector3.zero;
        if(Input.GetKey(KeyCode.W)) moveDirection += anchor.forward;
        if(Input.GetKey(KeyCode.S)) moveDirection += -anchor.forward;
        if(Input.GetKey(KeyCode.A)) moveDirection += -anchor.right;
        if(Input.GetKey(KeyCode.D)) moveDirection += anchor.right;
        moveDirection.y = 0.0F;
    }

    private void MoveCharacter() {
        // set rigidbody speed instead of changing position
        GetComponent<Rigidbody>().velocity = moveDirection.normalized * moveSpeed;

        if(moveDirection != Vector3.zero) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision col) {
        // use for tracking towns and such
        if(col.gameObject.CompareTag("Wall")) {
            //Debug.Log("Ouch");
            
        }
        
    }
}
