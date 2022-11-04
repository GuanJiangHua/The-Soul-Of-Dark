using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCameraManager : MonoBehaviour
{
    public float cameraSpeed = 1;
    public Transform[] cameraPoints;

    bool isCanMove = false;
    Transform currentPoint;

    private void Start()
    {
        SetCurrentPointByID(0);
    }
    private void Update()
    {
        if(isCanMove == true)
        {
            MoveToCurrentPoint();
        }
    }
    public void SetCurrentPointByID(int id)
    {
        isCanMove = true;
        currentPoint = cameraPoints[id];
    }

    private void MoveToCurrentPoint()
    {
        Vector3 disVector = transform.position - currentPoint.position;
        float distance = Vector3.SqrMagnitude(disVector);

        if(distance < 0.1f)
        {
            isCanMove = false;
        }

        transform.position = Vector3.Lerp(transform.position, currentPoint.position, cameraSpeed * Time.deltaTime);
    }
}
