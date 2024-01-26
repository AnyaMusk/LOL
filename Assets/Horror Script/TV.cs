using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class TV : Interactable
{
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject antennaGameObject;
    [SerializeField] private VideoClip[] videoClip;
    [SerializeField] private Key.KeyType typeOfKeyRequired;
    [SerializeField] private float idleTimeDuration;
    
    private bool isInteractedOnce;
    private VideoPlayer videoPlayer;

    public override void Start()
    {
        isInteractedOnce = false;
        antennaGameObject.SetActive(false);
        //videoPlayer = screen.GetComponent<VideoPlayer>();
        //videoPlayer.clip = videoClip[0];
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        if(isInteractedOnce) return;
        CheckPlayerHandForItem(itemsInPlayerHand);
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "TV";
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

                antennaGameObject.SetActive(true);

                // unlock anim or any animation and then destroy it
                // TODO : change video player
                //videoPlayer.clip = videoClip[1];

                Destroy(items.gameObject);
                // door Unlocked text
                UIManager.Instance.DialogueTextManipulation($"TV is on");
            }
            else
            {
                // door locked text
                if(typeOfKeyRequired == Key.KeyType.None) return;
                UIManager.Instance.DialogueTextManipulation($"{typeOfKeyRequired} is needed to get signal");

            }
        }
        else
        {
            if(typeOfKeyRequired == Key.KeyType.None) return;
            UIManager.Instance.DialogueTextManipulation($"{typeOfKeyRequired} is needed to get signal");
        }
    }
}
