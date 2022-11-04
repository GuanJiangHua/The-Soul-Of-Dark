using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName ="�µ��˹�����Ϊ",menuName ="A.I/������Ϊ/�½�������Ϊ")]
    public class EnemyAttackAction : EnemyAction
    {
        public bool canCombo;              //��������?
        public EnemyAttackAction comboAttack;

        public int attackScore = 3;         //��������?
        public float recoveryTime = 2;   //�������ʱ��;

        public float maxAttackAngle = 35;
        public float minAttackAngle = -35;

        public float minDistanceNededToAttack = 0;  //������Ҫ����С����
        public float maxDistanceNededToAttack = 3; //������Ҫ��������
    }
}
