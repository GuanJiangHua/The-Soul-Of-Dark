using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CameraHandler : MonoBehaviour
    {
        [Header("�����������:")]
        public float maxLockOnDistance = 20;
        public CharacterManager nearestLockOnTarger;                                                                 //���������Ŀ��;
        public CharacterManager currentLockOnTarger;                                                                 //��ǰ����Ŀ��;
        public CharacterManager leftLockTarget;                                                                            //�൱ǰ����Ŀ�����߾������������Ŀ��
        public CharacterManager rightLockTarget;                                                                          //�൱ǰ����Ŀ����ұ߾������������Ŀ��
        List<CharacterManager> avilableTargets = new List<CharacterManager>();         //�Ϸ�������Ŀ���б�

        [Header("�����������:")]
        public Transform targetTransform;       //����������Ŀ��
        public Transform targetTransformWhileAiming;    //��׼ʱ������������transform
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        public Camera cameraObject;
        private Vector3 cameraTransformPosition;
        public LayerMask ignoreLayer;
        public LayerMask enviromentLayer;
        private Vector3 cameraFollowVelocity = Vector3.zero;

        public static CameraHandler singleton;
        public float leftAndRightLookSpeed = 250f;
        public float leftAndRightAimingLookSpeed = 25f;
        public float upAndDownLookSpeed = 250f;           //������ת�ٶ�;
        public float upAndDownAimingLookSpeed = 25f;
        public float followSpeed = 0.1f;           //�����ٶ�;


        private float targetPosition;                  //
        private float defaultPosition;                //Ĭ��λ��;
        private float leftAndRightAngle;                        //���ҽǶ�;
        private float upAndDownAngle;                     //���½Ƕ�;

        public float minimumUpAngle=-35;                  //����ĽǶ���С����
        public float maximumUpAngle=35;                   //����ĽǶ��������

        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffset = 0.2f;      //�������ײƫ��;
        public float minimumCollisionOffset = 0.2f;  //�������ײ����Сƫ��;
        public float lockPivotPosition = 2.25f;           //��������λ��;
        public float unlockedPivotPosition = 1.65f;   //��������λ��;

        InputHandler inputHandler;
        PlayerManager playerManger;
        private void Awake()
        {
            singleton = this;
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            playerManger = FindObjectOfType<PlayerManager>();
            cameraObject = GetComponentInChildren<Camera>();
            defaultPosition = cameraTransform.position.z;
            ignoreLayer = ~(1 << 9 | 1 << 10 | 1 << 12 | 1 << 13 | 1<< 14 | 1<<15 |1<< 16);         //~��ȡ������;   layer��24λ�Ķ��������ݣ�ÿһλ��ʾÿһ����Ӧ��ŵ�ͼ��(����8��9��10��������в�)

            inputHandler = FindObjectOfType<InputHandler>();
        }

        private void Start()
        {
            enviromentLayer = LayerMask.NameToLayer("Enviroment");
        }
        //����Ŀ��(��Ҷ���)�ķ���:
        public void FollowTarget()
        {
            if (playerManger.isAiming)
            {
                Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransformWhileAiming.position , ref cameraFollowVelocity, Time.deltaTime * 25);
                transform.position = targetPosition;
                cameraTransform.localPosition = Vector3.zero;
            }
            else
            {
                Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity , Time.deltaTime * followSpeed);
                transform.position = targetPosition;
            }

            HandleCameraCollision();
        }

        //�������ת:
        public void HandleCameraRotation()
        {
            if(inputHandler.lockOnFlag == true && currentLockOnTarger != null)
            {
                HandleLockedCameraRotation();
            }
            else if(playerManger.isAiming)
            {
                HandleAimingCameraRotation();
            }
            else
            {
                HandleStandardCameraRotation();
            }
        }
        //�������ת����:(��׼��ת)
        public void HandleStandardCameraRotation()
        {
            leftAndRightAngle += (inputHandler.mouseX * leftAndRightLookSpeed) * Time.deltaTime;
            upAndDownAngle -= (inputHandler.mouseY * upAndDownLookSpeed) * Time.deltaTime;
            upAndDownAngle = Mathf.Clamp(upAndDownAngle, minimumUpAngle, maximumUpAngle);

            Vector3 rotation = Vector3.zero;
            rotation.y = leftAndRightAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = upAndDownAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;

        }
        //�������ת����:(������ת)
        private void HandleLockedCameraRotation()
        {
            if(currentLockOnTarger != null)
            {
                Vector3 dir = currentLockOnTarger.transform.position - targetTransform.position;
                dir.Normalize();
                dir.y = 0;

                Quaternion targerRotation = Quaternion.LookRotation(dir);
                transform.rotation = targerRotation;

                dir = currentLockOnTarger.transform.position - cameraPivotTransform.position;
                dir.Normalize();

                targerRotation = Quaternion.LookRotation(dir);
                Vector3 eulerAngle = targerRotation.eulerAngles;
                eulerAngle.x = Mathf.Clamp(eulerAngle.x, minimumUpAngle, maximumUpAngle);
                eulerAngle.y = 0;
                cameraPivotTransform.localEulerAngles = eulerAngle;
            }
        }
        //�������ת����:(��׼��ת)
        private void HandleAimingCameraRotation()
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            cameraPivotTransform.rotation = Quaternion.Euler(0, 0, 0);

            Quaternion targetRotationX;
            Quaternion targetRotationY;

            Vector3 cameraRotationX = Vector3.zero;
            Vector3 cameraRotationY = Vector3.zero;

            leftAndRightAngle += (inputHandler.mouseX * leftAndRightAimingLookSpeed) * Time.deltaTime;
            upAndDownAngle -= (inputHandler.mouseY * upAndDownAimingLookSpeed) * Time.deltaTime;
            upAndDownAngle = Mathf.Clamp(upAndDownAngle, 2 * minimumUpAngle, 2 * maximumUpAngle);

            cameraRotationY.y = leftAndRightAngle;
            targetRotationY = Quaternion.Euler(cameraRotationY);
            targetRotationY = Quaternion.Slerp(transform.rotation, targetRotationY, 1);
            transform.localRotation = targetRotationY;

            cameraRotationX.x = upAndDownAngle;
            targetRotationX = Quaternion.Euler(cameraRotationX);
            targetRotationX = Quaternion.Slerp(cameraPivotTransform.localRotation , targetRotationX, 1);

            cameraPivotTransform.localRotation = targetRotationX;
        }

        //���������:
        private void HandleCameraCollision()
        {
           targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position,cameraSphereRadius,direction,out hit, Mathf.Abs(targetPosition), ignoreLayer))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);     //��������λ�õ���ײ�����λ�õľ���;
                targetPosition = -(dis - cameraCollisionOffset);                                           //�����ȥ���ƫ�Ƶ��෴��������������zֵ���ܵ��뾶��Χ���������ײ����Ӱ�죩;
            }

            if (Mathf.Abs(targetPosition) < minimumCollisionOffset)     
            {
                targetPosition = -minimumCollisionOffset;
            }

            if(playerManger.isAiming == false)
            {
                cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, Time.deltaTime * 5f);
                cameraTransform.localPosition = cameraTransformPosition;
            }
        }
    
        //���������Ŀ��:
        public void HandleLockOn()
        {
            leftLockTarget = null;
            rightLockTarget = null;
            float shortestDistance = Mathf.Infinity;        //��ʼ������̾��뿪ʼΪ�������;
            float shortestDistanceOfLeftTarget = Mathf.Infinity;
            float shortestDistanceOfRightTarget = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);     //��ȡ������Ŀ��ΪԲ�ģ��뾶26��λ�ڵ�ʹ����ײ��
            //ѡ�����кϷ�Ŀ��
            for(int i = 0; i < colliders.Length; i++)
            {
                int playerIDName = playerManger.GetComponent<PlayerStatsManager>().teamIDNumber;
                CharacterManager chaeacter = colliders[i].GetComponent<CharacterManager>();

                if (chaeacter != null)
                {
                    CharacterStatsManager characterStats = chaeacter.GetComponent<CharacterStatsManager>();

                    Vector3 lockTargetDirection = chaeacter.transform.position - targetTransform.position;          //����Ŀ�������Ŀ��������
                    float distanceFromTarget = Vector3.Distance(chaeacter.transform.position , targetTransform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);             //���������������ǰ�������ļн�;
                    RaycastHit hit;

                    //�ж�����:���������Ǹ������ && ���������ǰ���ļн���50��֮�� && С�������������
                    if(chaeacter.transform.root != targetTransform.root && viewableAngle > -50 && viewableAngle < 50 && distanceFromTarget <= maxLockOnDistance)
                    {
                        //�������ߣ��ж϶����Ƿ��ڵ�
                        if(Physics.Linecast(playerManger.RayCastStartPoint.position , chaeacter.lockOnTransform.position , out hit))
                        {
                            Debug.DrawLine(playerManger.RayCastStartPoint.position, chaeacter.lockOnTransform.position,Color.white);
                            if (hit.transform.gameObject.layer == enviromentLayer || characterStats.teamIDNumber == playerIDName)
                            {
                                //�޷�����Ŀ�꣬����ķ�ʽ
                            }
                            else
                            {
                                avilableTargets.Add(chaeacter);
                            }
                        }
                    }
                }
            }

            //ѡ�����Ŀ��:
            for(int k = 0; k < avilableTargets.Count; k++)
            {
                float distanceFromTarger = Vector3.SqrMagnitude(targetTransform.position - avilableTargets[k].transform.position);
                if(distanceFromTarger < shortestDistance)
                {
                    shortestDistance = distanceFromTarger;
                    if(avilableTargets[k].GetComponent<EnemyStatsManager>().isDead == false)
                    {
                        nearestLockOnTarger = avilableTargets[k];
                    }
                }

                if(inputHandler.lockOnFlag == true)
                {
                    //Vector3 relativeEnemyPosition = currentLockOnTarger.transform.InverseTransformPoint(avilableTargets[k].transform.position);   //��������������λ��;
                    //var distanceFromLeftTarget = currentLockOnTarger.transform.position.x + avilableTargets[k].transform.position.x;
                    //var distanceFromRightTarget = currentLockOnTarger.transform.position.x - avilableTargets[k].transform.position.x;

                    Vector3 relativeEnemyPosition = inputHandler.transform.InverseTransformPoint(avilableTargets[k].transform.position);   //��������������λ��;
                    var distanceFromLeftTarget =Mathf.Abs(relativeEnemyPosition.x);
                    var distanceFromRightTarget = relativeEnemyPosition.x;
                    //�ж�����:�������ڵ�ǰ����Ŀ���λ�õ�x����0(�����) && �Ⱦ൱ǰ����Ŀ��������С���뻹С
                    if (relativeEnemyPosition.x <= 0.00 && distanceFromLeftTarget < shortestDistanceOfLeftTarget && avilableTargets[k] != currentLockOnTarger)
                    {
                        shortestDistanceOfLeftTarget = distanceFromLeftTarget;
                        leftLockTarget = avilableTargets[k];
                    }

                    if (relativeEnemyPosition.x >= 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget && avilableTargets[k] != currentLockOnTarger)
                    {
                        shortestDistanceOfRightTarget = distanceFromRightTarget;
                        rightLockTarget = avilableTargets[k];
                    }
                }
            }
        }
    
        //ȡ������Ŀ��:
        public void ClearLockOnTargets()
        {
            avilableTargets.Clear();
            nearestLockOnTarger = null;
            currentLockOnTarger = null;
        }
    
        //�ı�������߶�:
        public void SetCameraHeight()
        {
            Vector3 velocity = Vector3.zero;
            Vector3 newLockedPosition = new Vector3(0, lockPivotPosition);
            Vector3 newUnlockedPosition = new Vector3(0, unlockedPivotPosition);

            if(currentLockOnTarger != null)
            {
                cameraPivotTransform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.localPosition, newLockedPosition, ref velocity, Time.deltaTime);
                cameraPivotTransform.localRotation = Quaternion.Euler(2, 0, 0);
            }
            else
            {
                cameraPivotTransform.localPosition = Vector3.SmoothDamp(cameraPivotTransform.localPosition, newUnlockedPosition, ref velocity, Time.deltaTime);
                cameraPivotTransform.localRotation = Quaternion.Euler(2, 0, 0);
            }
        }
    }
}
