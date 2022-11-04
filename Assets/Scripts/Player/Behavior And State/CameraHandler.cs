using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class CameraHandler : MonoBehaviour
    {
        [Header("锁定相关属性:")]
        public float maxLockOnDistance = 20;
        public CharacterManager nearestLockOnTarger;                                                                 //最近的锁定目标;
        public CharacterManager currentLockOnTarger;                                                                 //当前锁定目标;
        public CharacterManager leftLockTarget;                                                                            //距当前锁定目标的左边距离最近的锁定目标
        public CharacterManager rightLockTarget;                                                                          //距当前锁定目标的右边距离最近的锁定目标
        List<CharacterManager> avilableTargets = new List<CharacterManager>();         //合法的锁定目标列表

        [Header("跟随相关属性:")]
        public Transform targetTransform;       //摄像机跟随的目标
        public Transform targetTransformWhileAiming;    //瞄准时摄像机跟随这个transform
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
        public float upAndDownLookSpeed = 250f;           //上下旋转速度;
        public float upAndDownAimingLookSpeed = 25f;
        public float followSpeed = 0.1f;           //跟随速度;


        private float targetPosition;                  //
        private float defaultPosition;                //默认位置;
        private float leftAndRightAngle;                        //左右角度;
        private float upAndDownAngle;                     //上下角度;

        public float minimumUpAngle=-35;                  //枢轴的角度最小限制
        public float maximumUpAngle=35;                   //枢轴的角度最大限制

        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffset = 0.2f;      //摄像机碰撞偏移;
        public float minimumCollisionOffset = 0.2f;  //摄像机碰撞的最小偏移;
        public float lockPivotPosition = 2.25f;           //锁定枢轴位置;
        public float unlockedPivotPosition = 1.65f;   //解锁枢轴位置;

        InputHandler inputHandler;
        PlayerManager playerManger;
        private void Awake()
        {
            singleton = this;
            targetTransform = FindObjectOfType<PlayerManager>().transform;
            playerManger = FindObjectOfType<PlayerManager>();
            cameraObject = GetComponentInChildren<Camera>();
            defaultPosition = cameraTransform.position.z;
            ignoreLayer = ~(1 << 9 | 1 << 10 | 1 << 12 | 1 << 13 | 1<< 14 | 1<<15 |1<< 16);         //~是取反运算;   layer是24位的二进制数据，每一位表示每一个对应序号的图层(除了8，9，10以外的所有层)

            inputHandler = FindObjectOfType<InputHandler>();
        }

        private void Start()
        {
            enviromentLayer = LayerMask.NameToLayer("Enviroment");
        }
        //跟随目标(玩家对象)的方法:
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

        //摄像机旋转:
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
        //摄像机旋转方法:(标准旋转)
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
        //摄像机旋转方法:(锁定旋转)
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
        //摄像机旋转方法:(瞄准旋转)
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

        //摄像机避障:
        private void HandleCameraCollision()
        {
           targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();

            if(Physics.SphereCast(cameraPivotTransform.position,cameraSphereRadius,direction,out hit, Mathf.Abs(targetPosition), ignoreLayer))
            {
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);     //相机枢轴的位置到碰撞对象的位置的距离;
                targetPosition = -(dis - cameraCollisionOffset);                                           //距离减去相机偏移的相反数（摄像机距离的z值，受到半径范围内最近的碰撞器的影响）;
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
    
        //摄像机锁定目标:
        public void HandleLockOn()
        {
            leftLockTarget = null;
            rightLockTarget = null;
            float shortestDistance = Mathf.Infinity;        //初始化，最短距离开始为，无穷大;
            float shortestDistanceOfLeftTarget = Mathf.Infinity;
            float shortestDistanceOfRightTarget = Mathf.Infinity;

            Collider[] colliders = Physics.OverlapSphere(targetTransform.position, 26);     //获取到跟随目标为圆心，半径26单位内的使用碰撞体
            //选出所有合法目标
            for(int i = 0; i < colliders.Length; i++)
            {
                int playerIDName = playerManger.GetComponent<PlayerStatsManager>().teamIDNumber;
                CharacterManager chaeacter = colliders[i].GetComponent<CharacterManager>();

                if (chaeacter != null)
                {
                    CharacterStatsManager characterStats = chaeacter.GetComponent<CharacterStatsManager>();

                    Vector3 lockTargetDirection = chaeacter.transform.position - targetTransform.position;          //锁定目标与跟随目标间的向量
                    float distanceFromTarget = Vector3.Distance(chaeacter.transform.position , targetTransform.position);
                    float viewableAngle = Vector3.Angle(lockTargetDirection, cameraTransform.forward);             //方向向量与摄像机前方向量的夹角;
                    RaycastHit hit;

                    //判断条件:锁定对象不是跟随对象 && 其与摄像机前方的夹角在50度之内 && 小于最大锁定距离
                    if(chaeacter.transform.root != targetTransform.root && viewableAngle > -50 && viewableAngle < 50 && distanceFromTarget <= maxLockOnDistance)
                    {
                        //连接射线，判断对象是否被遮挡
                        if(Physics.Linecast(playerManger.RayCastStartPoint.position , chaeacter.lockOnTransform.position , out hit))
                        {
                            Debug.DrawLine(playerManger.RayCastStartPoint.position, chaeacter.lockOnTransform.position,Color.white);
                            if (hit.transform.gameObject.layer == enviromentLayer || characterStats.teamIDNumber == playerIDName)
                            {
                                //无法锁定目标，对象的方式
                            }
                            else
                            {
                                avilableTargets.Add(chaeacter);
                            }
                        }
                    }
                }
            }

            //选出最近目标:
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
                    //Vector3 relativeEnemyPosition = currentLockOnTarger.transform.InverseTransformPoint(avilableTargets[k].transform.position);   //相对于锁定对象的位置;
                    //var distanceFromLeftTarget = currentLockOnTarger.transform.position.x + avilableTargets[k].transform.position.x;
                    //var distanceFromRightTarget = currentLockOnTarger.transform.position.x - avilableTargets[k].transform.position.x;

                    Vector3 relativeEnemyPosition = inputHandler.transform.InverseTransformPoint(avilableTargets[k].transform.position);   //相对于锁定对象的位置;
                    var distanceFromLeftTarget =Mathf.Abs(relativeEnemyPosition.x);
                    var distanceFromRightTarget = relativeEnemyPosition.x;
                    //判断条件:如果相对于当前锁定目标的位置的x大于0(在左边) && 比距当前锁定目标的左边最小距离还小
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
    
        //取消锁定目标:
        public void ClearLockOnTargets()
        {
            avilableTargets.Clear();
            nearestLockOnTarger = null;
            currentLockOnTarger = null;
        }
    
        //改变摄像机高度:
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
