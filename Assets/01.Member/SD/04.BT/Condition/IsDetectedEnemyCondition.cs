using _01.Member.SD._01.Code.Unit.Interface;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "IsDetectedEnemy", story: "Is Detected [Enemy]", category: "Conditions", id: "717f60542727636dd0c328ecee528bf7")]
public partial class IsDetectedEnemyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Target> Enemy;

    public override bool IsTrue()
    {
        if (Enemy.Value != null) 
        {
            return true;        
        }
        return false;
    }

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
