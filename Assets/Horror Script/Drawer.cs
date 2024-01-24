using TMPro;
using UnityEngine;

public class Drawer : Interactable
{
    private Animator animator;
    private bool isOpen;
    private static readonly int Open = Animator.StringToHash("Open");
    public override void Start()
    {
        isOpen = false;
        animator = GetComponent<Animator>();
    }

    public override void Interact(ref Items itemsInPlayerHand)
    {
        isOpen = !isOpen;
        animator.SetBool(Open, isOpen);
    }

    public override void InteractAlternate(ref Items itemsInPlayerHand, ref Interactable interactableInHand)
    {
       // not 
    }

    public override void VisibleUI(bool isVisible)
    {
        UIManager.Instance.instructionText.SetActive(isVisible);
        if (UIManager.Instance.instructionText.activeInHierarchy)
        {
            UIManager.Instance.instructionText.GetComponent<TextMeshProUGUI>().text = "[E] \n Open";
        }
    }
}
