using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public string idleParameterName = "IdleAnimation";
    public string runningParameterName = "IsRunning";
    public string busyParameterName = "IsBusy";
    public string fightAnimationName = "horse_rear_up";
    public AudioClip footstepLoop;
    public int numIdleAnimations = 4;
    public int chanceForSpecial = 2;
    public float idleChangeDelay = 3;
    public float movingThreshold = 0.5f;
    public float footstepVolume = 0.5f;
    public float footstepPitch = 0.5f;
    
    private int idleParameter;
    private int runningParameter;
    private int busyParameter;
    private Animator animator;
    private AudioSource soundEmitter;
    private Transform myTransform;
    
    private bool isRunning = false;
    //private bool isBusy;
    
    void Start() {
        animator = GetComponent<Animator>();
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
        busyParameter = Animator.StringToHash(busyParameterName);
        
        // Lazy solution to random horse idle animations - just changing it every 3 seconds
        // Better way would be to add Animation Events but I don't feel like dealing with that
        InvokeRepeating("PickNewIdleAnimation", 0, idleChangeDelay);
    }
    
    void Update() {
        if(isRunning && !soundEmitter.isPlaying) {
            soundEmitter.Play();
        } else if(!isRunning && soundEmitter.isPlaying) {
            soundEmitter.Pause();
        }
    }
    
    public void SetRunning(bool flag) {
        isRunning = flag;
        animator.SetBool(runningParameter, flag);
    }
    
    public void PlayFightAnimation() {
        animator.Play("Base Layer." + fightAnimationName);
    }
    
    private void PickNewIdleAnimation() {
        int animationIndex = 0;
        if(Random.Range(0, chanceForSpecial) == 0) {
            animationIndex = Random.Range(0, numIdleAnimations) + 1;
        }        
        animator.SetInteger(idleParameterName, animationIndex);
    }
}
