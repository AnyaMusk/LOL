using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float happyTimeDuration;
    [SerializeField] private float grumpyTimerDuration;
    [SerializeField] private Vector2 delay;


    private float happyTimer;
    private float grumpyTimer;
    [HideInInspector]
    public float idleTimer;
    [HideInInspector]
    public GhostStates currentState;

    [SerializeField]private AudioClip[] audioClip;
    private AudioSource audioSource;

    private bool playSound = false;

  
    private float timeDelay;
    

    public static GameManager Instance;
    public enum GhostStates
    {
        Idle,
        Happy,
        Grumpy,
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentState = GhostStates.Idle;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GhostStates.Idle:
                happyTimer = happyTimeDuration;
                grumpyTimer = grumpyTimerDuration;
                idleTimer -= Time.deltaTime;
                
                if (idleTimer <= 0)
                {
                    currentState = GhostStates.Happy;
                }
                break;
            
            case GhostStates.Happy:
                happyTimer -= Time.deltaTime;
                
                // playing sounds 
                if (playSound == false)
                {
                    StartCoroutine(StartFlickering());
                }

                if (happyTimer <= 0)
                {
                    // ghost starts being grumpy
                    currentState = GhostStates.Grumpy;
                }
                break;
            
            case GhostStates.Grumpy:
                grumpyTimer -= Time.deltaTime;
                if (grumpyTimer <= 0)
                {
                    // game over


                }
                break;
        }
        
    }
    
    private IEnumerator StartFlickering()
    {
        playSound = true;
        timeDelay = Random.Range(delay.x, delay.y);
        yield return new WaitForSeconds(timeDelay);
        audioSource.PlayOneShot(audioClip[0]);
        timeDelay = Random.Range(delay.x, delay.y);
        yield return new WaitForSeconds(timeDelay);
        playSound = false;
    }
    public void PlayHappySound()
    {
        audioSource.PlayOneShot(audioClip[1]);
    }
}
