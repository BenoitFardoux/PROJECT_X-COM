using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    [Header("User actions")]
    [SerializeField] private InputActionReference moveCameraAction;

    [Header("Camera settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera mainCamera;


    private Vector3 direction = Vector3.zero;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }
    private void OnEnable()
    {
        moveCameraAction.action.performed += OnMoveCamera;
        moveCameraAction.action.canceled += context => direction = Vector3.zero;
        moveCameraAction.action.Enable();
    }

    private void OnDisable()
    {
        moveCameraAction.action.performed -= OnMoveCamera;
        moveCameraAction.action.canceled -= context => direction = Vector3.zero;
        moveCameraAction.action.Disable();
    }


    private void OnMoveCamera(InputAction.CallbackContext context)
    {


        Vector2 input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0, input.y);

    }

    public void Update()
    {
        mainCamera.transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
    }
}
