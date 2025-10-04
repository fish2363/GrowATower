using _01.Member.SD._01.Code.Unit.Interface;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RotationToTarget", story: "[Unit] Rotation To [Target]", category: "Action", id: "fb030b50592955ae0f17cb70d82649e4")]
public partial class RotationToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<BaseUnit> Unit;
    [SerializeReference] public BlackboardVariable<Target> Target;
    
    [SerializeField] private float rotationSpeed;

    protected override Status OnStart()
    {
        rotationSpeed = Unit.Value.unitStat.rotationSpeed;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Unit.Value == null || Target.Value == null)
            return Status.Failure;

        Transform unitTransform = Unit.Value.transform;
        Transform targetTransform = Target.Value.transform;

      
        Vector3 direction = (targetTransform.position - unitTransform.position).normalized;

        if (direction == Vector3.zero)
            return Status.Success; 

     
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

      
        unitTransform.rotation = Quaternion.Slerp(
            unitTransform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );

   
        float angle = Quaternion.Angle(unitTransform.rotation, targetRotation);
        if (angle < 1f)
            return Status.Success;

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}