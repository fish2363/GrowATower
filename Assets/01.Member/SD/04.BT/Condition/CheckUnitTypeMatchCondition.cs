using _01.Member.SD._01.Code.Unit.Interface;
using System;
using _01.Member.SD._01.Code.Unit.Interface.UnitSupport;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(
    name: "CheckUnitTypeMatch",
    story: "Check if [Unit] type is (or is not) [TargetType] [InvertResult]",
    category: "Conditions",
    id: "20904e7f8744ed910996831f16d0364d"
)]
public partial class CheckUnitTypeMatchCondition : Condition
{
    [SerializeReference] public BlackboardVariable<BaseUnit> Unit;
    [SerializeReference] public BlackboardVariable<UnitType> TargetType;
    [SerializeReference] public BlackboardVariable<bool> InvertResult;

    public override bool IsTrue()
    {
        if (Unit?.Value == null || TargetType?.Value == null)
            return false;

        bool isSame = Unit.Value.unitStat.type == TargetType.Value;
        return InvertResult.Value ? isSame : !isSame;
    }

    public override void OnStart() { }
    public override void OnEnd() { }
}