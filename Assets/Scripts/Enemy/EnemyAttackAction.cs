using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    [CreateAssetMenu(fileName ="新敌人攻击行为",menuName ="A.I/敌人行为/新建攻击行为")]
    public class EnemyAttackAction : EnemyAction
    {
        public bool canCombo;              //可以连击?
        public EnemyAttackAction comboAttack;

        public int attackScore = 3;         //攻击分数?
        public float recoveryTime = 2;   //攻击间隔时间;

        public float maxAttackAngle = 35;
        public float minAttackAngle = -35;

        public float minDistanceNededToAttack = 0;  //攻击需要的最小距离
        public float maxDistanceNededToAttack = 3; //攻击需要的最大距离
    }
}
