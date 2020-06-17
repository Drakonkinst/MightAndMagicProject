using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTransform : MonoBehaviour {

    private string moveInputAxis = "Vertical";
    private string turnInputAccess = "Horizontal";
    public float rotationRate = 360; // degrees/sec
    public float moveSpeed = 1; // units/sec

    void Update() {
        float moveAxis = Input.GetAxis(moveInputAxis);
        float turnAxis = Input.GetAxis(turnInputAccess);
        ApplyInput(moveAxis, turnAxis);
    }

    private void ApplyInput(float moveInput, float turnInput) {
        Move(moveInput);
        Turn(turnInput);
    }

    private void Move(float input) {
        transform.Translate(Vector3.forward * input * moveSpeed);
    }

    private void Turn(float input) {
        transform.Rotate(0, input * rotationRate * Time.deltaTime, 0);
    }

}