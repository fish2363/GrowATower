using _01.Member.SD._01.Code.Unit.Interface;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UnitAttack", story: "[Unit] Attack to [Target]", category: "Action", id: "f613abfefc55421bbf76610bc84beb01")]
public partial class UnitAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<CanAttackUnit> Unit;
    [SerializeReference] public BlackboardVariable<Target> Target;

    protected override Status OnStart()
    {
        Unit.Value.baseAttck.Attack(Unit,Target.Value);
        return base.OnStart();
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

