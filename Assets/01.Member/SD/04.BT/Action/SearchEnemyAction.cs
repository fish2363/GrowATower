using _01.Member.SD._01.Code.Unit.Interface;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SearchEnemyAction", story: "[Unit] Find [Enemy] In Can Delect [Range]", category: "Action", id: "c941ceea90ece94c7ffe6ccd0cc172f8")]
public partial class SearchEnemyAction : Action
{
    [SerializeReference] public BlackboardVariable<CanAttackUnit> Unit;
    [SerializeReference] public BlackboardVariable<float> Range;
    [SerializeReference] public BlackboardVariable<Target> Enemy;
    private LayerMask targetLayer;
    
    protected override Status OnStart()
    {
        targetLayer = LayerMask.GetMask("Enemy");
        return base.OnStart();
    }

    protected override Status OnUpdate()
    {
        if (Unit?.Value == null) 
            return Status.Failure;

        Vector3 unitPos = Unit.Value.transform.position;
        Collider[] enemies = Physics.OverlapSphere(unitPos, Range.Value, targetLayer);

        if (enemies.Length == 0)
            return Status.Failure;

        Target selectedEnemy = null;
        
        Array.Sort(Unit.Value.EnemyPriorities, (a, b) => a.priority.CompareTo(b.priority));
        
        foreach (var priority in Unit.Value.EnemyPriorities)
        {
            Target bestTarget = null;

            foreach (Collider enemy in enemies)
            {
                Target target = enemy.GetComponent<Target>();
                if (target == null) continue;
                
                if (target.enemyType != priority.enemType) continue;

            
                if (bestTarget == null || target.Pos.x > bestTarget.Pos.x)
                {
                    bestTarget = target;
                }
            }
            
            if (bestTarget != null)
            {
                selectedEnemy = bestTarget;
                break;
            }
        }

        Enemy.Value = selectedEnemy;

        return Status.Success;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Unit.Value.transform.position, Range.Value);
    }
}

