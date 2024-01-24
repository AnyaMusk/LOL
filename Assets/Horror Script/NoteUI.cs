using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class NoteUI : MonoBehaviour
{
    [SerializeField] private GameObject playerController;
    [SerializeField] private GameObject playerHUDUI;
   
    [SerializeField] private TextMeshProUGUI message;
    
    public void Exit()
    {
        gameObject.SetActive(false);
        playerHUDUI.SetActive(true);
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.GetComponent<PlayerController>().enabled = true;
        playerController.GetComponent<Interaction>().enabled = true;
    }
    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            playerHUDUI.SetActive(false);
            playerController.GetComponent<CharacterController>().enabled = false;
            playerController.GetComponent<PlayerController>().enabled = false;
            playerController.GetComponent<Interaction>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SetText(string text)
    {
        message.text = text;
    }
}
