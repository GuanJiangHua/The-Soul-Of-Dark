using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheTarget : MonoBehaviour
{
    public Transform target;
    [Header("¸úËæËÙ¶È:")]
    public float followSpeed = 1;
    public float rotateSpeed = 1;
    [Header("ÊÇ·ñ¸úËæ:")]
    public bool isFollow = true;

    private void Update()
    {
        if (isFollow)
        {
            FollowMoveAndRotate();
        }
    }

    private void FollowMoveAndRotate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotateSpeed);
    }
}
