using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //public Player player;
    
    public static SceneManager Instance { get; private set; } // static singleston
    
    void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        
        //player = FindObjectOfType<Player>();
    }
}
