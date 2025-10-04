using UnityEngine;
using Entities;
using GondrLib.ObjectPool.RunTime;

namespace Enemies
{
    public class Enemy : Entity, IPoolable
    {
        [SerializeField] private EnemyStatSO statSO;
        public EnemyStatSO StatSO { get { return statSO; } }

        [field:SerializeField] public PoolItemSO PoolItem {get; private set;}
        public GameObject GameObject => gameObject;

        private Pool _pool;

        public void ResetItem()
        {
            transform.position = Vector3.zero;
        }

        public void SetUpPool(Pool pool)
        {
            _pool = pool;
        }
    }
}