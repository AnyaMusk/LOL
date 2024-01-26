using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Robot : Interactable
{
    [SerializeField] Animator animator;
    [SerializeField] private float idleTimeDuration;
    private AudioSource audioSource;

    [SerializeField] private Key.KeyType typeOfKeyRequired;
    [SerializeField] private AudioClip[] audioClips;

    private bool isInteractedOnce;
    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isInteractedOnce = false;
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        if(isInteractedOnce) return;
        CheckPlayerHandForItem(itemsInPlayerHand);
        //animator.SetTrigger($"Run");
        
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        // no
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "Robot";
    }
    
    public void CheckPlayerHandForItem(Items items)
    {
        if (items is Key)
        {
            if ((items as Key).keyType == typeOfKeyRequired)
            {
                isInteractedOnce = true;
                GameManager.Instance.currentState = GameManager.GhostStates.Idle;
                GameManager.Instance.idleTimer = idleTimeDuration;
                GameManager.Instance.PlayHappySound();
                audioSource.PlayOneShot(audioClips[Random.Range(0,audioClips.Length - 1)]);
                // unlock anim or any animation and then destroy it
                Destroy(items.gameObject);
                // door Unlocked text
                UIManager.Instance.DialogueTextManipulation($"Robot is on");
            }
            else
            {
                // door locked text
                if(typeOfKeyRequired == Key.KeyType.None) return;
                UIManager.Instance.DialogueTextManipulation($"{typeOfKeyRequired} is needed to operate");

            }
        }
        else
        {
            if(typeOfKeyRequired == Key.KeyType.None) return;
            UIManager.Instance.DialogueTextManipulation($"{typeOfKeyRequired} is needed to operate");
        }
    }
}
