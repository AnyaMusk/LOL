using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GhostBehaviour : MonoBehaviour
{
    public GameObject ghostObject;
    private void Start()
    {
        ghostObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag($"PlayerParanormalTag"))
        {
            ghostObject.SetActive(true);
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag($"PlayerParanormalTag"))
        {
            ghostObject.SetActive(false);
        }
    }
}
