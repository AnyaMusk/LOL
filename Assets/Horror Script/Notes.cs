using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Notes : Interactable
{
    [Multiline(10)]
    public string textField;
    [SerializeField] private NoteUI noteUI;
    public override void Start()
    {
        noteUI.gameObject.SetActive(false);
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        noteUI.gameObject.SetActive(true);
        noteUI.SetText(textField);
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        // not implementing
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "[E] \n Read";
        }
    }
}
