using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class EnemyResurrectableManager : MonoBehaviour
    {
        //��ʼλ��:
        Vector3 originalPoint;
        //��ʼ��ת:
        Quaternion originalRotate;
        //��ʼ״̬:
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

            enemyManager.enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0, Time.deltaTime);        //���ƶ������Ĳ�������Ϊ1;
            enemyManager.enemyAnimatorManager.anim.SetFloat("Horizontal", 0, 0, Time.deltaTime);

            enemyManager.enemyAnimatorManager.PlayTargetAnimation("GetUp",false);
        }
    }
}