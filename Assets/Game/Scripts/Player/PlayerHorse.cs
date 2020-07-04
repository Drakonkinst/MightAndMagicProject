using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorse : MonoBehaviour
{
    public string idleParameterName = "IdleAnimation";
    public string runningParameterName = "IsRunning";
    public AudioClip footstepLoop;
    public int numIdleAnimations = 4;
    public int chanceForSpecial = 2;
    public float idleChangeDelay = 3;
    public float movingThreshold = 0.5f;
    public float footstepVolume = 0.5f;
    public float footstepPitch = 0.5f;
    
    private int idleParameter;
    private int runningParameter;
    private Animator animator;
    private Rigidbody rigidBody;
    private Transform myTransform;
    private AudioSource soundEmitter;
    //private Vector3 lastPos; 
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        
        myTransform = transform;
        GameObject obj = new GameObject("Horse Footsteps");
        obj.transform.position = myTransform.position;
        obj.transform.parent = myTransform;
        soundEmitter = obj.AddComponent<AudioSource>();
        soundEmitter.clip = footstepLoop;
        soundEmitter.volume = footstepVolume;
        soundEmitter.pitch = footstepPitch;
        soundEmitter.loop = true;
        
        idleParameter = Animator.StringToHash(idleParameterName);
        runningParameter = Animator.StringToHash(runningParameterName);
        // Lazy solution to random horse idle animations - just changing it every 3 seconds
        // Better way would be to add Animation Events but I don't feel like dealing with that
        InvokeRepeating("PickNewIdleAnimation", 0, idleChangeDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 difference = myTransform.position - lastPos;
        //bool isRunning = (difference.magnitude / Time.deltaTime) > movingThreshold;
        bool isRunning = rigidBody.velocity.magnitude > movingThreshold;
        animator.SetBool(runningParameter, isRunning);
        //Debug.Log("IS RUNNING: " + isRunning);
        if(isRunning && !soundEmitter.isPlaying) {
            soundEmitter.Play();
        } else if(!isRunning && soundEmitter.isPlaying) {
            soundEmitter.Pause();
        }
        //lastPos = myTransform.position;
    }
    
    private void PickNewIdleAnimation() {
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        int animationIndex = 0;
        if(Random.Range(0, chanceForSpecial) == 0) {
            animationIndex = Random.Range(0, numIdleAnimations) + 1;
        }        
        animator.SetInteger(idleParameterName, animationIndex);
    }
}
