using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyResurrectableManager : MonoBehaviour
    {
        //初始位置:
        Vector3 originalPoint;
        //初始旋转:
        Quaternion originalRotate;
        //初始状态:
        State originalState;

        EnemyManager enemyManager;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();

            originalPoint = transform.position;
            originalRotate = transform.rotation;

            originalState = enemyManager.currentState;
        }
        
        public void ReturnToOriginal()
        {
            transform.position = originalPoint;
            transform.rotation = originalRotate;

            enemyManager.currentTarget = null;
            enemyManager.currentState = originalState;
            enemyManager.enemyWeaponSlotManager.rightHandSlot.UnloadWeaponAndDestroy();

            enemyManager.enemyStats.ReturnToOriginalState();

            enemyManager.enemyAnimatorManager.anim.SetBool("isDead", false);

            enemyManager.enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0, Time.deltaTime);        //将移动动画的参数设置为1;
            enemyManager.enemyAnimatorManager.anim.SetFloat("Horizontal", 0, 0, Time.deltaTime);

            enemyManager.enemyAnimatorManager.PlayTargetAnimation("GetUp",false);
        }
    }
}