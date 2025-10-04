using System;
using System.Collections.Generic;
using UnityEngine;
using _01.Member.SD._01.Code.Unit.Interface.Attacks;
using _01.Member.SD._01.Code.Unit.Interface.UnitSupport;
using UnityEngine.Serialization;

namespace _01.Member.SD._01.Code.Unit.Interface
{
    public class BaseUnit:MonoBehaviour, IUnit
    {
        [FormerlySerializedAs("UnitStatSO")] public UnitStat unitStat;
     
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