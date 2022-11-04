using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    PlayerControls playerControls;
    Vector2 cameraInput;

    public float rotateAmount = 1;
    public float rotateSpeed = 5;
    [Header("是否可以旋转:")]
    public bool canRotateObject = false;

    Vector3 currentRotate;
    Vector3 targetRotate;

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }
    private void Start()
    {
        currentRotate = transform.eulerAngles;
        targetRotate = transform.eulerAngles;
    }
    private void Update()
    {
        if (canRotateObject)
        {
            if(cameraInput.x > 0)
            {
                targetRotate.y -= rotateAmount;
            }
            else if(cameraInput.x < 0)
            {
                targetRotate.y += rotateAmount;
            }

            currentRotate = Vector3.Lerp(currentRotate, targetRotate, rotateSpeed * Time.deltaTime);
            transform.eulerAngles = currentRotate;
        }
    }

    public void SetCanRotate(bool canRotate)
    {
        canRotateObject = canRotate;
    }
}
