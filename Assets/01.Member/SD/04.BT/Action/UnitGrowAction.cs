using _01.Member.SD._01.Code.Unit.Interface;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "UnitGrow", story: "[Unit] Grow [GrowUnit]", category: "Action", id: "1ebbd35db2c0e102f7891c3a1f2cb13b")]
public partial class UnitGrowAction : Action
{
    [SerializeReference] public BlackboardVariable<BaseUnit> Unit;
    [SerializeReference] public BlackboardVariable<GrowUnit> GrowUnit;
   
    protected override Status OnStart()
    {
        if (GrowUnit.Value == null)
        {
            GrowUnit component = Unit.Value.GetComponent<GrowUnit>();
            if (component == null)
                return Status.Failure;
            
            GrowUnit.Value = component;
        }

        GrowUnit.Value.OnTurnPassed();
        return Status.Success;
    }
}