using UnityEngine;
using UnityEngine.Serialization;

public class LookAtCameraUI : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;

    private enum Mode
    {
        LookAt,
        LookAtInverted,
        Forward,
        ForwardInverted,
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(playerCamera.transform);
                break;
            case Mode.LookAtInverted:
                //transform.LookAt(-(Camera.main.transform.position));
                Vector3 direction = transform.position - playerCamera.transform.position;
                transform.LookAt(transform.position + direction);
                break;
            case Mode.Forward:
                transform.forward =playerCamera.transform.forward;
                break;
            case Mode.ForwardInverted:
                transform.forward = -playerCamera.transform.forward;
                break;
        }
    }
}