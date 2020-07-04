using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip[] music;
    public float volume = 0.55f;
    
    private AudioSource currentMusic = null;
    private Transform myTransform;
    private int currentIndex = -1;

    void Start() {
        if(music == null || music.Length == 0) {
            Debug.LogWarning("No background music found!");
            return;
        }
        
        myTransform = transform;
        PlayNextSong();
        currentMusic.loop = true;
    }
    
    private void PlayNextSong() {
        AudioClip clip = ChooseRandomSong();
        Debug.Log("Playing " + clip.name);
        // Creates new object. Probably inefficient but I'm too tired to think of a natural system for now.
        // Maybe just use the native system for the background music? Keep an AudioSource object
        // Should still be a child component since good practice, in the case of ambient sounds or whatever
        currentMusic = SoundManager.Instance.Play(clip, myTransform, volume);
        Invoke("PlayNextSong", currentMusic.clip.length);
    }
    
    private AudioClip ChooseRandomSong() {
        int newIndex;
        do {
            newIndex = Random.Range(0, music.Length);
        } while(newIndex == currentIndex);
        return music[newIndex];
    }
}
