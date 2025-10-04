using System;
using System.Collections.Generic;
using UnityEngine;
using _01.Member.SD._01.Code.Unit.Interface.Attacks;
using _01.Member.SD._01.Code.Unit.Interface.UnitSupport;

namespace _01.Member.SD._01.Code.Unit.Interface
{
    public class BaseUnit:MonoBehaviour, IUnit
    {
        public BaseAttack baseAttck;
        public EnemyPriority[] EnemyPriorities;
    
        private void OnEnable()
        {
            
        }

        public virtual void OnPlace()
        {
            
        }

        public virtual void OnTurnPassed()
        {
          
        }

        public virtual void ExecuteAction()
        {
           
        }
    }
}