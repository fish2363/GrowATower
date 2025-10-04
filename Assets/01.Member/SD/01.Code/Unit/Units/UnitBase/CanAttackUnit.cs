using _01.Member.SD._01.Code.Unit.Interface.Attacks;
using _01.Member.SD._01.Code.Unit.Interface.UnitSupport;

namespace _01.Member.SD._01.Code.Unit.Interface
{
    public class CanAttackUnit:BaseUnit
    {
        public BaseAttack baseAttck;
        public EnemyPriority[] EnemyPriorities;
    }
}