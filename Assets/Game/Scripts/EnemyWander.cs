using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : MonoBehaviour
{
    private static readonly float MaxAngle = 2.0f * Mathf.PI;
    public float maxWanderDistance = 1.0f;
    public float minChangeDistance = 0.1f;
    public float maxChaseDistance = 3.0f;
    public float wanderSpeed = 1.0f;
    public float chaseSpeed = 2.0f;
    public float minPauseDuration = 1.5f;
    public float maxPauseDuration = 3.0f;

    private Vector3 center;
    private Vector3 target;
    private Transform myTransform;
    private bool isResting = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
        center = myTransform.position;
        target = center;
        SetNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Check for player within radius
        
        if(isResting) {
            return;
        }
        
        if(Vector3.Distance(myTransform.position, target) <= minChangeDistance) {
            SetNewTarget();
            StartCoroutine(Rest());
        } else {
            // move towards
            myTransform.position = Vector3.MoveTowards(myTransform.position, target, wanderSpeed * Time.deltaTime);
        }
    }
    
    private void SetNewTarget() {
        float a = Random.Range(0.0f, MaxAngle);
        float r = maxWanderDistance * Mathf.Sqrt(Random.value);
        target.x = r * Mathf.Cos(a) + center.x;
        target.z = r * Mathf.Sin(a) + center.z;
    }
    
    private IEnumerator Rest() {
        isResting = true;
        yield return new WaitForSeconds(Random.Range(minPauseDuration, maxPauseDuration));
        isResting = false;
    }
}
