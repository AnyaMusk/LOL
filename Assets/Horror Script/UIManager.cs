using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject instructionText;
    public GameObject canvas;
    public TextMeshProUGUI dialogueText;
    public GameObject blackImage;

    private Animator canvasAnimator;

    private void Awake()
    {
        Instance = this;
        canvasAnimator = canvas.GetComponent<Animator>();
    }

    private void Start()
    {
<<<<<<< Updated upstream
        
        blackImage.SetActive(true);
        canvasAnimator.SetTrigger("start");
        Invoke(nameof(SetImageActiveFalse), 2f);
=======
       /* canvasAnimator.SetTrigger("start");
        Invoke(nameof(SetImageActiveFalse), 2f);*/
>>>>>>> Stashed changes
    }


    public void DialogueTextManipulation(string message)
    {
        float alpha = 1;
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = message;
        DOVirtual.Float(1, 0, 4, value => alpha = value).OnComplete(() => dialogueText.gameObject.SetActive(false));
        dialogueText.color = new Color(dialogueText.color.r, dialogueText.color.g, dialogueText.color.b,
            alpha
        );

    }

    private void SetImageActiveFalse()
    {
        blackImage.SetActive(false);
    }
}
