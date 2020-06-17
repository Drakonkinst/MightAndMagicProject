using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWorld : MonoBehaviour
{
    public float moveSpeed = 5F; // units/sec
    public float rotationSpeed = 1000F;
    public Transform anchor;
    
    private PlayerControls controls;
    private Vector2 movementInput;
    private Vector3 moveDirection;
    private Transform myTransform;
    

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => {
            movementInput = ctx.ReadValue<Vector2>();
        };
    }

    void Start() {
        myTransform = transform;
    }

    void Update() {
        GetInput();
        MoveCharacter();
    }

    private void GetInput() {
        Vector2 moveInput = controls.Gameplay.Move.ReadValue<Vector2>();
        moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
    }

    private void MoveCharacter() {
        // set rigidbody speed instead of changing position
        // could remove .normalized for finer joystick input
        GetComponent<Rigidbody>().velocity = moveDirection.normalized * moveSpeed;

        if(moveDirection != Vector3.zero) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        // use for tracking towns and such
        if(other.tag == "Wall") {
            Debug.Log("Ouch");
            
        }

        if(other.tag == "Artifact") {
            Debug.Log("Yay!");
            Destroy(other.gameObject);
        } 
        
    }

    public void OnEnable() {
        if(controls != null) {
            controls.Enable();
        } else {
            Debug.LogWarning("No controls found on enabling!");
        }
    }

    public void OnDisable() {
        if(controls != null) {
            controls.Disable();
        } else {
            Debug.LogWarning("No controls found on disabling!");
        }
    }
}
