using _01.Member.SD._01.Code.Unit.Interface;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(
    name: "CheckRotationComplete",
    story: "[Unit] Check Rotation to [Enemy] Complete",
    category: "Conditions",
    id: "fc42b23b0e87411defc8c7f8c97f7352"
)]
public partial class CheckRotationCompleteCondition : Condition
{
    [SerializeReference] public BlackboardVariable<BaseUnit> Unit;
    [SerializeReference] public BlackboardVariable<Target> Enemy;
    
    [SerializeField] private float angleThreshold = 1f;

    public override bool IsTrue()
    {
        if (Unit.Value == null || Enemy.Value == null)
            return false;

        Transform unitTransform = Unit.Value.transform;
        Transform targetTransform = Enemy.Value.transform;

        Vector3 toTarget = (targetTransform.position - unitTransform.position).normalized;
        if (toTarget == Vector3.zero)
            return true; 

        Quaternion targetRotation = Quaternion.LookRotation(toTarget, Vector3.up);
        float angle = Quaternion.Angle(unitTransform.rotation, targetRotation);

        return angle < angleThreshold;
    }
}