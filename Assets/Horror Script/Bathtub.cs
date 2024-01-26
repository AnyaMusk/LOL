using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bathtub : Interactable
{
    [SerializeField] private GameObject electricityObject;
    public override void Start()
    {
        
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        return;
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        return;
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            if (electricityObject.activeInHierarchy)
            {
                UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "There is something inside the tub \n Gotta switch off the heater first";
            }
            else
            {
                UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "";

            }
        }
        
    }
}
