using System;
using _01.Member.SD._01.Code.Unit.Interface.Attacks;
using _01.Member.SD._01.Code.Unit.Interface.UnitSupport;
using UnityEngine;

namespace _01.Member.SD._01.Code.Unit.Interface
{
    public class CanAttackUnit:BaseUnit
    {
        public float attackRange;
        public BaseAttack baseAttck;
        public EnemyPriority[] EnemyPriorities;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position,attackRange);
        }
    }
    
   
}