using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraPos;
    private void LateUpdate()
    {
        transform.position = cameraPos.position;
        //transform.rotation = cameraPos.rotation;
    }



}