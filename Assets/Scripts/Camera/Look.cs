using UnityEngine;
using UnityEngine.InputSystem;

public class Look : MonoBehaviour
{
    private PlayerControls controls;

    [HideInInspector] public bool canRotateCamera;

    [SerializeField] private Camera cam;
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    private const float smoothing = 0.1f;

    [HideInInspector] public Transform orientation;

    private Vector2 inputVector;
    private float xRot;
    private float yRot;

    private void Start()
    {
        controls = new PlayerControls();
        controls.Camera.Enable();

        controls.Camera.FreeCursor.performed += ControlCursor;
        //UpdateFOV();
    }
    private void OnDestroy()
    {
        controls.Camera.FreeCursor.performed -= ControlCursor;
    }


    private void Update()
    {
        GetInput();
        UpdateCamera();
    }

    private void GetInput()
        => inputVector = controls.Camera.Look.ReadValue<Vector2>();

    //public void UpdateFOV()
    //{
    //    cam.fieldOfView = Settings.FOV;
    //}


    private void UpdateCamera()
    {
        if(!cam || !canRotateCamera)
            return;

        // Laggy beauty
        yRot += inputVector.x * 0.01f * sensX;//Settings.sensetivity;
        xRot -= inputVector.y * 0.01f * sensY;//Settings.sensetivity;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        //orientation.rotation = Quaternion.Euler(0, yRot, 0);
    }

    public void SetRotation(float x, float y)
    {
        xRot = x;
        yRot = y;
    }


    //public void SetupEvents()
    //{
    //    player.OnDeath += DisableLook;
    //    player.OnRespawn += EnableLook;

    //}

    private void ControlCursor(InputAction.CallbackContext context)
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            canRotateCamera = false;
        }
        else if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            canRotateCamera = true;
        }
    }
}

