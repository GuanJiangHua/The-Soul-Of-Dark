using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class Elevator : MonoBehaviour
    {
        public float averageSpeed = 1;
        public AnimationCurve speedDistributionCurve;
        [Header("上升台:")]
        public Transform ascendingPlatform;
        [Header("是顶部:")]
        public bool isTop = false;
        [Header("顶部位置:")]
        public Transform topTransform;
        [Header("底部位置:")]
        public Transform bottomTransform;

        Transform targetPoint;

        public void LiftTableInitialization(bool isInTop)
        {
            if (isInTop)
            {
                isTop = true;
                ascendingPlatform.position = topTransform.position;
            }
            else
            {
                isTop = false;
                ascendingPlatform.position = bottomTransform.position;
            }
        }
        public void StartingMechanism(LiftingTableSwitchInteractable mySwitch)
        {
            isTop = !isTop;
            switch (isTop)
            {
                case true:
                    targetPoint = topTransform;
                    break;
                case false:
                    targetPoint = bottomTransform;
                    break;
            }

            StartCoroutine(MoveToTarget(mySwitch));
        }

        //移动到目标:
        IEnumerator MoveToTarget(LiftingTableSwitchInteractable mySwitch)
        {
            mySwitch.interactableCollider.enabled = false;
            float distance = Vector3.SqrMagnitude(ascendingPlatform.position - targetPoint.position);
            float totalLength = distance;
            float distanceRatio = (totalLength - distance) / totalLength;
            float speedRatio = speedDistributionCurve.Evaluate(distanceRatio);
            while (distance > 0.01f)
            {
                ascendingPlatform.position = Vector3.Lerp(ascendingPlatform.position, targetPoint.position, Time.deltaTime * averageSpeed * speedRatio);

                distance = Vector3.SqrMagnitude(ascendingPlatform.position - targetPoint.position);
                distanceRatio = (totalLength - distance) / totalLength;

                yield return null;
            }
            mySwitch.interactableCollider.enabled = true;
        }
    }
}