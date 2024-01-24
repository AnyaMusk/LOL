using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassCodeDoorUI : MonoBehaviour
{
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject passCodeUI;
    [SerializeField] private GameObject playerHUDUI;

    public GameObject door;
    private Animator doorAnimator;

    public TextMeshProUGUI digits;
    public string answer = "223311";

    private AudioSource AudioSource;
    public AudioClip buttonClick;
    public AudioClip correctAnswer;
    public AudioClip wrongAnswer;
    

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        doorAnimator = door.GetComponent<Animator>();
    }

    public void Number(int number)
    {
        digits.text += number.ToString();
        AudioSource.PlayOneShot(buttonClick);
    }

    public void Execute()
    {
        if (digits.text == answer)
        {
            AudioSource.PlayOneShot(correctAnswer);
            digits.text = "Unlocked";
        }
        else
        {
            AudioSource.PlayOneShot(wrongAnswer);
            digits.text = "Wrong";
        }
    }

    public void Clear()
    {
        digits.text = "";
        AudioSource.PlayOneShot(buttonClick);
    }

    public void Exit()
    {
        passCodeUI.SetActive(false);
        playerHUDUI.SetActive(true);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<PlayerController>().enabled = true;
        playerController.GetComponent<Interaction>().enabled = true;
    }

    private void Update()
    {
        if (digits.text == "Unlocked")
        {
            // animate door
            doorAnimator.SetTrigger($"Open");
        }

        if (passCodeUI.activeInHierarchy)
        {
            playerHUDUI.SetActive(false);
            playerController.GetComponent<CharacterController>().enabled = false;
            playerController.GetComponent<PlayerController>().enabled = false;
            playerController.GetComponent<Interaction>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
