using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeaserSwitch : Interactable
{
    [SerializeField] private GameObject electricityPlate;
    [SerializeField] private AudioClip[] audioClips;

    private bool isOn;
    private AudioSource audioSource;
    public override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        electricityPlate.SetActive(true);
        isOn = true;
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        isOn = !isOn;
        electricityPlate.SetActive(isOn);
        audioSource.PlayOneShot(audioClips[Random.Range(0,audioClips.Length - 1)]);

    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        //no
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "[E] \n On/Off";

        }
    }
}
