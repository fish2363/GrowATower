using Unity.Behavior;

namespace _01.Member.SD._01.Code.Unit.Interface.UnitSupport
{
    [BlackboardEnum]
    public enum UnitState
    {
        PLACE = 1,// 설치 되었을때
        IDLE = 2, //타겟을 찾고 있으면 ATTACK, 없으면 IDLE 유지
        ATTACK = 3,//공격 유닛은 공격
        PRODUCE = 4,
        GROW = 5
    }
}