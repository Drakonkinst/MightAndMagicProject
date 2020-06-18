using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWorldMovement : MonoBehaviour
{
    public float moveSpeed = 5F;
    public float rotationSpeed = 1000F;
    
    private PlayerControls controls;
    private Vector2 movementInput;
    private Vector3 moveDirection;
    private Transform myTransform;
    private Rigidbody rigidBody;

    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => {
            movementInput = ctx.ReadValue<Vector2>();
        };
    }

    void Start() {
        myTransform = transform;
        rigidBody = GetComponent<Rigidbody>();
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
        rigidBody.velocity = moveDirection.normalized * moveSpeed;

        // turns player in direction of movement, with a small delay that does not affect movement
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
