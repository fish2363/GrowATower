using UnityEngine;

namespace _01.Member.SD._01.Code.Unit.Interface.Attacks
{
    public class BaseAttack : MonoBehaviour,IUnitAttackAction
    {
        public virtual void Attack(BaseUnit unit, Target target)
        {
          
        }
    }
}