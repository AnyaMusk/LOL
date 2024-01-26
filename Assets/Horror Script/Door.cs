using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Door : Interactable
{
    [SerializeField] Animator animator;
    private bool isOpen;
    private static readonly int Open = Animator.StringToHash("open");
    private AudioSource audioSource;
    private bool isLocked;

    [SerializeField] private Key.KeyType typeOfKeyRequired;
    [SerializeField] private AudioClip[] audioClips;
    

    public override void Start()
    {
        isLocked = true;
        isOpen = false;
        
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact(ref Items items)
    {
        // check for key in player hand
        CheckPlayerHandForItem(items);
        if (!isLocked)
        {
            isOpen = !isOpen;
            animator.SetBool(Open, isOpen);
            audioSource.PlayOneShot(audioClips[Random.Range(0,audioClips.Length - 1)]);
        }
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        // cant be done as not a usable item
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            if (isLocked)
            {
                UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "[E] \n Unlock";

            }
            else
            {
                UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "[E] \n Open/Close";
            }
        }
    }

    public void CheckPlayerHandForItem(Items items)
    {
        if (typeOfKeyRequired == Key.KeyType.None) isLocked = false;
        if (items is Key)
        {
            if ((items as Key).keyType == typeOfKeyRequired)
            {
                isLocked = false;
                // unlock anim or any animation and then destroy it
                Destroy(items.gameObject);
                // door Unlocked text
                UIManager.Instance.DialogueTextManipulation($"unlocked");
            }
            else
            {
                // door locked text
                if(typeOfKeyRequired == Key.KeyType.None) return;
                UIManager.Instance.DialogueTextManipulation($"{typeOfKeyRequired} is needed to unlock");

            }
        }
        else
        {
            if(typeOfKeyRequired == Key.KeyType.None) return;
            UIManager.Instance.DialogueTextManipulation($"{typeOfKeyRequired} is needed to unlock");
        }
    }
}
