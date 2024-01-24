using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float interactionMaxDis = 5f;
    [SerializeField] private int interactibleLayerMask;
    [SerializeField] private int interactibleKeyLayerMask;

    private Interactable selectedInteractable; // item were looking at
    private Interactable usableItem; // item in inventory
    private Items itemsInPlayerHand; // item in inventory

    private void Start()
    {
        InputManager.Instance.OnInteractionPressed += InteractWhenPressed;
        InputManager.Instance.OnInteractionAlternatePressed += InteractAltWhenPressed;
    }

    private void InteractWhenPressed()
    {
        if(selectedInteractable == null) return;
        selectedInteractable.Interact(ref itemsInPlayerHand);
    }
    private void InteractAltWhenPressed()
    {
        if (PlayerHasItem())
        {
            // trying to pickup when something in hand
            if (usableItem != null)
            {
                UIManager.Instance.DialogueTextManipulation("Has a item in Hand");
                return;
            }
            // To drop
            itemsInPlayerHand.Drop(null, ref itemsInPlayerHand,ref usableItem);
            return;
        }
        // use for pick up when hands empty
        if (usableItem != null) usableItem.InteractAlternate(ref itemsInPlayerHand, ref usableItem);
    }

    private void Update()
    {
        GetTheObjectInFront();
    }

    private void GetTheObjectInFront()
    {
        
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * interactionMaxDis);
        if (Physics.Raycast(ray, out RaycastHit hit,interactionMaxDis, (1 << interactibleLayerMask) | (1 << interactibleKeyLayerMask)))
        {
            if (hit.transform.TryGetComponent(out Interactable interactable))
            {
                interactable.VisibleUI(true);
                if (selectedInteractable != interactable)
                {
                    selectedInteractable = interactable;
                }
                
                // check if interactive is a item
                if (interactable is Items)
                {
                    // pick up or drop
                    usableItem = interactable;
                }
            }
            else
            {
                if(selectedInteractable != null) selectedInteractable.VisibleUI(false);
                usableItem = null;
            }
        }
        else
        {
            if(selectedInteractable != null) selectedInteractable.VisibleUI(false);
            usableItem = null;
        }
       
    }

    private bool PlayerHasItem()
    {
        return itemsInPlayerHand != null;
    }
    
}
