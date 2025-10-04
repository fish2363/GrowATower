using UnityEngine;
using Entities;
using System;
namespace Enemies
{
    public class MoveComponent : MonoBehaviour, IEntityComponent
    {
        private EnemyStatSO stat;
        private Enemy _owner;
        public void Initialize(Entity owner)
        {
            _owner = owner as Enemy;
            if(_owner != null)
            {
                stat = _owner.StatSO;
            }
            else
                throw new ArgumentNullException(stat.name);
        }


    }
}