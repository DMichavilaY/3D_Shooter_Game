using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLookCamera : MonoBehaviour
{
    public float sensitivity = 2.0f;
    public float verticalLimit = 80f;

    private PlayerControls controls;
    private float verticalRotation = 0f;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Look.performed += ctx => RotateCamera(ctx.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void RotateCamera(Vector2 lookInput)
    {
        float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLimit, verticalLimit);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
