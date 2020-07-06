using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerAnimations animations;
    public CameraFollow camera;
    public float moveSpeed = 6.0f;
    public float rotationSpeed = 1000.0f;
    public float instanceSwitchDelay = 3.0f;
    public float movingThreshold = 0.5f;
    
    private PlayerControls controls;
    private Vector2 movementInput;
    private Vector3 moveDirection;
    private Transform myTransform;
    private Rigidbody rigidBody;
    
    private bool isInControl;
    
    void Awake() {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += ctx => {
            movementInput = ctx.ReadValue<Vector2>();
        };
    }
    
    void Start()
    {
        myTransform = transform;
        rigidBody = GetComponent<Rigidbody>();
        isInControl = true;
    }

    void Update()
    {
        if(isInControl) {
            GetInput();
            MoveCharacter();
        }
        animations.SetRunning(rigidBody.velocity.magnitude > movingThreshold);
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
            
        } else if(other.tag == "Artifact") {
            Debug.Log("Yay!");
            Destroy(other.gameObject);
        } else if(other.tag == "Enemy") {
            Debug.Log("Time to fight!");
            StartCoroutine(BeginCombat());
        } else if(other.tag == "Structure") {
            Debug.Log("Time to go inside!");
            StartCoroutine(EnterStructure());
            // TODO: Stop world, do zooming animation for ~3s, change scene
            // TODO: Make sure they respawn facing away + a small distance away from the structure
            // TODO: All structures are about the same size
        }
    }
    
    private IEnumerator EnterStructure() {
        float prevDistance = camera.playerDistance;
        float height = camera.height;
        camera.playerDistance = 5.0f;
        camera.height = 6.0f;
        isInControl = false;
        rigidBody.velocity = Vector3.zero;
        yield return new WaitForSeconds(instanceSwitchDelay);
        isInControl = true;
        camera.playerDistance = prevDistance;
        camera.height = height;
    }
    
    private IEnumerator BeginCombat() {
        float prevDistance = camera.playerDistance;
        float height = camera.height;
        camera.playerDistance = 5.0f;
        camera.height = 6.0f;
        isInControl = false;
        rigidBody.velocity = Vector3.zero;
        animations.PlayFightAnimation();
        yield return new WaitForSeconds(instanceSwitchDelay);
        isInControl = true;
        camera.playerDistance = prevDistance;
        camera.height = height;
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
