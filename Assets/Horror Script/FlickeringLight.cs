using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FlickeringLight : MonoBehaviour
{
    public GameObject lightObject;
    private bool flickering;
    private float timeDelay;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float delay = 0.2f;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lightObject.SetActive(false);
        flickering = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag($"PlayerParanormalTag"))
        {
            if (flickering == false)
            {
                StartCoroutine(StartFlickering());
            }
        }
    }

    private IEnumerator StartFlickering()
    {
        flickering = true;
        lightObject.SetActive(false);
        timeDelay = Random.Range(0.01f, delay);
        yield return new WaitForSeconds(timeDelay);
        lightObject.SetActive(true);
        audioSource.PlayOneShot(audioClip);
        timeDelay = Random.Range(0.01f, delay);
        yield return new WaitForSeconds(timeDelay);
        flickering = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag($"PlayerParanormalTag"))
        {
            lightObject.SetActive(false);
        }
    }
}
