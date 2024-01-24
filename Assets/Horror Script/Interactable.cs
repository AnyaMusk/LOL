using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Start();

    public abstract void Interact(ref Items itemsInPlayerHand);
    public abstract void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand);
    public abstract void VisibleUI(bool isVisible);
}
