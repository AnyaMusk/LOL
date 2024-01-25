using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : Interactable
{
    [SerializeField] private GameObject batteries;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private bool isBroken;
    public override void Start()
    {
        rb = GetComponent<Rigidbody>();
        batteries.SetActive(false);
        isBroken = false;
        rb.isKinematic = true;
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        if(isBroken) return;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        Invoke(nameof(BreakClock), 0.83f);
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
        // nahi
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            if (!isBroken)
            {
                UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = $"[E] \n Clock";
            }
            else
            {
                UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = $" Broken Clock";
            }
        }
    }

    private void BreakClock()
    {
        isBroken = true;
        batteries.SetActive(true);
    }
}
