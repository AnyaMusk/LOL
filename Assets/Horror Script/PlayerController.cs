using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    [Header("Mouse")]
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] float turnFactor = 0.1f;

    [Header("HeadBob")]
    [SerializeField] bool headBobEnable = true;
    [SerializeField, Range(0.001f, 0.01f)] float amount;
    //[SerializeField] private float amplitude = 0.015f; 
    //[SerializeField, Range(100, 150)] private float smooth = 100; 
    [SerializeField, Range(10, 100)] private float smooth = 80; 
    [SerializeField, Range(1, 30)] private float frequency ;
    [SerializeField] public Transform cameraPos;
    [SerializeField] public Transform Playercamera;
    internal float bobMultiplier;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float accleration = 20f;
    [SerializeField] internal float gravityValue = 20f;

    [Header("footStep")] 
    [SerializeField] private float baseFootStep = 0.5f;
    [SerializeField] private AudioSource footStepSource;
    [SerializeField] private AudioClip[] footstepClips;

    private float footStepTimer;

    private CharacterController player;
    
    internal Vector2 moveInput;

    float xRotation;
    float yRotation;
    //float multiplier = 0.01f;
    Vector3 startPos;
    Quaternion startRot;
    
    // for velocity direcn
    internal Vector3 velocity;
    
    

    //groundebool
    public bool isGrounded => player.isGrounded;
    private bool wasGrounded;
    public event Action<bool> OnGroundStateChanged;

    public float Height
    {
        get { return player.height;}
        set { player.height = value; } 
    }

    private void Awake()
    {
        player = GetComponent<CharacterController>();
    }

    private void Start(){
        DisableCursor();
        inputManager = InputManager.Instance;
        startPos = cameraPos.localPosition;
        startRot = cameraPos.localRotation;

    }

    private void Update(){
        UpdateGround();
        UpdateGravity();
        UpdateLook();
        UpdateMovement();
        if (headBobEnable)
        {
            HeadBob();
            ResetHeadBob();
        }

        HandleFootStep();
    }

    private void UpdateGround()
    {
        if(wasGrounded != isGrounded)
        {
            OnGroundStateChanged?.Invoke(isGrounded);
            wasGrounded = isGrounded;
        }
    }

    private void UpdateLook(){
        Vector2 mouse = inputManager.GetMouseDelta() ;
        //this rotations are for character
        yRotation += mouse.x * mouseSensitivity ;
        xRotation -= mouse.y * mouseSensitivity ;
        xRotation = Mathf.Clamp(xRotation, -40f, 70f);

        //cameraHolder.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraHolder.transform.rotation = Quaternion.Slerp(cameraHolder.transform.rotation, 
            Quaternion.Euler(xRotation, yRotation, 0), turnFactor);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private Vector3 GetMoveInput()
    {
        moveInput = inputManager.GetMoveVector();
        Vector3 moveDir = new Vector3(moveInput.x, 0, moveInput.y);
        moveDir = orientation.forward * moveDir.z + orientation.right * moveDir.x;
        moveDir.y = 0;


        moveDir *= moveSpeed;
        //transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.World);
        //transform.position += moveDir * moveSpeed * Time.deltaTime;

        return moveDir;
    }

    private void UpdateMovement()
    {
        Vector3 input = GetMoveInput();
        var factor = accleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);
        player.Move(velocity * Time.deltaTime);
        
    }

    void UpdateGravity()
    {
        var gravity = gravityValue * mass * Time.deltaTime;
        velocity.y = player.isGrounded ? 0 : velocity.y + gravity;
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void HeadBob()
    {
        if (!player.isGrounded) return;
        if (moveInput.magnitude > 0)
        {
            StartHeadBob();
            if (moveInput.x == 1 || moveInput.x > 0.5f)
            {
                //cameraPos.Rotate(cameraHolder.forward, 50 * Time.deltaTime);
                cameraPos.transform.localRotation = Quaternion.Slerp(cameraPos.transform.localRotation,
                    Quaternion.Euler(cameraPos.localRotation.x, cameraPos.localRotation.y, cameraPos.localRotation.z + 5), 5* Time.deltaTime);




            }
            else if (moveInput.x == -1 || moveInput.x < -0.5f)
            {
                //camera.transform.localRotation = Quaternion.Euler(camera.transform.localRotation.x, camera.transform.localRotation.y, camera.transform.localRotation.z - 15);
                cameraPos.transform.localRotation = Quaternion.Slerp(cameraPos.transform.localRotation,
                    Quaternion.Euler(cameraPos.localRotation.x, cameraPos.localRotation.y, cameraPos.localRotation.z - 5), 5 * Time.deltaTime);
            }
        }
    }
    private Vector3 StartHeadBob()
    {
        bobMultiplier = 1;
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency * bobMultiplier) * amount * bobMultiplier * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency * 0.5f * bobMultiplier) * amount * bobMultiplier * 1.6f, smooth * Time.deltaTime);
        cameraPos.localPosition += pos;
        return pos;
    }
    private void ResetHeadBob() 
    {
        if (cameraPos.localPosition == startPos) return;
        if(cameraPos.localRotation == startRot ) return;
        cameraPos.localPosition = Vector3.Lerp(cameraPos.localPosition, startPos, 1 * Time.deltaTime);
        cameraPos.localRotation = Quaternion.Slerp(cameraPos.localRotation, startRot, 5 * Time.deltaTime);
    }

    private void HandleFootStep()
    {
        if (moveInput != Vector2.zero)
        {
            footStepTimer -= Time.deltaTime;

            if (footStepTimer <= 0)
            {
                footStepSource.PlayOneShot(footstepClips[UnityEngine.Random.Range(0, footstepClips.Length - 1)]);
                footStepTimer = baseFootStep;
            }
        }
    }

}
