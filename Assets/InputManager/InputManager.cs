using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 inputVector;
    private Vector2 lookVector;
    
    private static InputManager _instance;
    public static InputManager Instance
    {
        get { return _instance; }
    }

    public Action OnInteractionPressed;
    public Action OnInteractionAlternatePressed;

    private void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
        
    }

    private void OnEnable(){
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.Player.Movement.performed += ctx => inputVector = ctx.ReadValue<Vector2>();
            playerControls.Player.Look.performed += (ctx) => lookVector = ctx.ReadValue<Vector2>();
            playerControls.Player.Interact.performed += InteractOnPerformed;
            playerControls.Player.InteractAlternate.performed += InteractAlternateOnperformed;
        }
        playerControls.Enable();
    }

    public void InteractAlternateOnperformed(InputAction.CallbackContext obj)
    {
        OnInteractionAlternatePressed?.Invoke();
    }

    public void InteractOnPerformed(InputAction.CallbackContext obj)
    {
        OnInteractionPressed?.Invoke();
    }

    private void OnDisable(){
        playerControls.Disable();
    }


    public Vector2 GetMoveVector()
    {
        return inputVector;
    }

    public Vector2 GetMouseDelta()
    {
        return lookVector;
    }
}