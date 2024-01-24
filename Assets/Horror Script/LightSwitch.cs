using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LightSwitch : Interactable
{
   [SerializeField] private GameObject lightGameObject;

   private bool isOn;

   public override void Start()
   {
        lightGameObject.SetActive(false);
   }

   public override void Interact(ref Items itemsInPlayerHand)
   {
       isOn = !isOn;
       lightGameObject.SetActive(isOn);
   }

   public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
   {
     // cant be implemented
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
