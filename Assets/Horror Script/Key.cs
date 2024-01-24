using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;

public class Key : Items
{
    private Rigidbody rb;
    [SerializeField] private Transform parentOfItem;
    

    public KeyType keyType;
    public override void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Interact(ref Items items)
    {
        // use to open locks or doors
        Use();
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        Pick(this, ref itemsInPlayerHand, ref interactableInHand);
    }

    public override void VisibleUI(bool isVisible)
    {
        //canvasGameObject.SetActive(isVisible);
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "[F] \n Pick/Drop";
        }
    }

    public override void Pick(Items items, ref Items itemsInPlayerHand,  ref Interactable usableItem)
    {
        itemsInPlayerHand = items;
        usableItem = this;
        transform.SetParent(parentOfItem);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
    }

    public override void Use()
    {
        
    }
    public override void Drop(Items items, ref Items itemsInPlayerHand, ref Interactable usableItem)
    {
        itemsInPlayerHand = items;
        usableItem = null;
        transform.SetParent(null);
        rb.isKinematic = false;
    }
    
    public enum KeyType
    {
        MasterKey,
        BedroomKey,
        None,
    }
}
