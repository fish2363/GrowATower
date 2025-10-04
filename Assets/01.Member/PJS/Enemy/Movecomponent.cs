using Entities;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Enemies
{
    public class MoveComponent : MonoBehaviour, IEntityComponent
    {
        private EnemyStatSO stat;
        private Enemy _owner;

        private List<Transform> path;
        private int CurrentWayPointIdx = 0;
        private Vector3 lastPosition;
        public void Initialize(Entity owner)
        {
            _owner = owner as Enemy;
            if(_owner != null)
            {
                stat = _owner.StatSO;
            }
            else
                throw new InvalidCastException($"fail to cast {owner.GetType().Name} to {nameof(Enemy)}");

            CurrentWayPointIdx = 0;
        }
        void Start()
        {
            if (WayPointManager.Instance.waypoints != null)
            {
                path = WayPointManager.Instance.waypoints;
            }

            // 2.  rigidbody 컴포넌트 확인 및 설정 권장
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogWarning("there is no rigid");
            }
            else
            {
                // Rigidbody가 있다면, 보간 설정 확인을 권장
                if (rb.interpolation == RigidbodyInterpolation.None)
                {
                    rb.interpolation = RigidbodyInterpolation.Interpolate;
                }
            }
        }
        void FixedUpdate()
        {
            if (path == null || path.Count == 0 || CurrentWayPointIdx >= path.Count) return;

            Transform targetWaypoint = path[CurrentWayPointIdx];

            Vector3 toTarget = targetWaypoint.position - transform.position;
            Vector3 direction = toTarget.normalized;
            float moveDistance = stat.speed * Time.fixedDeltaTime;

            Vector3 newPosition = transform.position + direction * moveDistance;

            if (moveDistance >= toTarget.magnitude)
            {
                transform.position = targetWaypoint.position;

                CurrentWayPointIdx++;
            }
            else
            {
                transform.position = newPosition;
            }

            lastPosition = transform.position;
        }
    }
}