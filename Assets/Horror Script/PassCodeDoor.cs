using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassCodeDoor : Interactable
{
    [SerializeField] private GameObject passCodeUI;

    public override void Start()
    {
        passCodeUI.SetActive(false);
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        passCodeUI.SetActive(true);
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        // not implementable
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "[E] \n EnterCode";
        }
    }
}
