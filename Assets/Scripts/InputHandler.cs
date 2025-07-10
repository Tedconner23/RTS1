using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Reference to the input actions
    private PlayerActions inputActions;
    public CameraHandler cameraHandler;

    private Vector3 moveDirectionX;
    private Vector3 moveDirectionY;
    private Vector3 moveDirectionZ;
    private Vector3 rotateDirectionY;

    private Vector3 scrollRateLocal;

    void Awake()
    {
        // Initialize the input actions
        inputActions = new PlayerActions();
        scrollRateLocal = CommonVariables.ScrollRate;

        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        // Register event handlers for performed actions
        inputActions.MapMove.Forward.performed += ctx => moveDirectionY += (Vector3.forward * scrollRateLocal.y);
        inputActions.MapMove.Back.performed += ctx => moveDirectionY += (Vector3.back * scrollRateLocal.y);
        inputActions.MapMove.Left.performed += ctx => moveDirectionX += (Vector3.left * scrollRateLocal.x);
        inputActions.MapMove.Right.performed += ctx => moveDirectionX += (Vector3.right * scrollRateLocal.x);
        inputActions.MapMove.ScrollDown.performed += ctx => moveDirectionZ += (Vector3.down * scrollRateLocal.z);
        inputActions.MapMove.ScrollUp.performed += ctx => moveDirectionZ += (Vector3.up * scrollRateLocal.z);
        inputActions.MapMove.CameraRotateCW.performed += ctx => rotateDirectionY += Vector3.up;
        inputActions.MapMove.CameraRotateCCW.performed += ctx => rotateDirectionY += Vector3.down;

        // Register event handlers for canceled actions
        inputActions.MapMove.Forward.canceled += ctx => moveDirectionY = Vector3.zero;
        inputActions.MapMove.Back.canceled += ctx => moveDirectionY = Vector3.zero;
        inputActions.MapMove.Left.canceled += ctx => moveDirectionX = Vector3.zero;
        inputActions.MapMove.Right.canceled += ctx => moveDirectionX = Vector3.zero;
        inputActions.MapMove.ScrollDown.canceled += ctx => moveDirectionZ = Vector3.zero;
        inputActions.MapMove.ScrollUp.canceled += ctx => moveDirectionZ = Vector3.zero;
        inputActions.MapMove.CameraRotateCW.canceled += ctx => rotateDirectionY = Vector3.zero;
        inputActions.MapMove.CameraRotateCCW.canceled += ctx => rotateDirectionY = Vector3.zero;
    }

    void UpdateScrollRate()
    {
        // Update the scroll rate and re-register input actions
        scrollRateLocal = CommonVariables.ScrollRate;
        RegisterInputActions();
    }

    void Update()
    {
        if (scrollRateLocal != CommonVariables.ScrollRate)
        {
            UpdateScrollRate();
        }

        if (moveDirectionY != Vector3.zero)
        {
            cameraHandler.MoveCamera(moveDirectionY * Time.deltaTime);
        }
        if (moveDirectionX != Vector3.zero)
        {
            cameraHandler.MoveCamera(moveDirectionX * Time.deltaTime);
        }
        if (moveDirectionZ != Vector3.zero)
        {
            cameraHandler.MoveCamera(moveDirectionZ * Time.deltaTime);
        }
        if (rotateDirectionY != Vector3.zero)
        {
            cameraHandler.RotateCamera(rotateDirectionY * Time.deltaTime);
        }
    }

    void OnEnable()
    {
        // Enable the input actions
        inputActions.Enable();
    }

    void OnDisable()
    {
        // Disable the input actions
        inputActions.Disable();
    }
}
