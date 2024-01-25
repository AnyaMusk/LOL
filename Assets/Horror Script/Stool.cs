using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Stool : Items
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform parentOfItem;
    [SerializeField] private Transform placeStoolTransform;
    [SerializeField] private BoxCollider boxTriggerCollider;
    private BoxCollider boxCollider;

    public override void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxTriggerCollider = GetComponent<BoxCollider>();
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        Pick(this, ref itemsInPlayerHand, ref interactableInHand);
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = $"[F] \n Stool";
        }
    }

    public override void Pick(Items items, ref Items itemsInPlayerHand, ref Interactable usableItem)
    {
        itemsInPlayerHand = items;
        usableItem = this;
        transform.SetParent(parentOfItem);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        boxCollider.enabled = false;
        boxTriggerCollider.enabled = false;
        characterController.radius = 1.5f;
    }

    public override void Use()
    {
      
    }

    public override void Drop(Items items, ref Items itemsInPlayerHand, ref Interactable usableItem)
    {
        itemsInPlayerHand = items;
        usableItem = null;
        transform.SetParent(null);

        transform.position = placeStoolTransform.position;
        transform.rotation = Quaternion.identity;
        boxCollider.enabled = true;
        boxTriggerCollider.enabled = true;
        
        characterController.radius = 1f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"PlayerParanormalTag"))
        {
            characterController.stepOffset = 2f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag($"PlayerParanormalTag"))
        {
            characterController.stepOffset = 0.3f;
            
        }
    }
}
