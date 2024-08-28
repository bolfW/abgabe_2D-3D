using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInput : MonoBehaviour
{
    [SerializeField] float sensitivity = 0.03f;
    Vector3 localEulerAngleInput;

    GameControlls cameraInputs;
    InputAction look;

    private void Awake()
    {
        cameraInputs = new GameControlls();
    }

    void OnEnable()
    {
        look = cameraInputs.Camera.Look;

        look.Enable();
    }

    void OnDisable()
    {
        look.Disable();
    }

    void Update()
    {
        localEulerAngleInput.x -= look.ReadValue<Vector2>().y*sensitivity;
        localEulerAngleInput.x = Mathf.Clamp(localEulerAngleInput.x, -90f, 90f);

        localEulerAngleInput.y += look.ReadValue<Vector2>().x*sensitivity;

        while (localEulerAngleInput.y > 360)
        {
            localEulerAngleInput.y -= 360;
        }

        transform.localEulerAngles = localEulerAngleInput;
    }
}
