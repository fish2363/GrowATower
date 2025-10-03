using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private EnemyStatSO statSO;

        public EnemyStatSO StatSO { get { return statSO; } }
        public bool IsDead { get; set; }

        Dictionary<Type, IEntityComponent> _components;

        public T GetCompo<T>() where T : IEntityComponent
              => (T)_components.GetValueOrDefault(typeof(T));

        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IEntityComponent>();
            AddComponents();
            InitializeComponents();
            AfterInitialize();
        }

        private void AfterInitialize()
        {
            _components.Values.OfType<IAfterInitialize>()
                .ToList().ForEach(compo => compo.AfterInitialize());
        }

        private void InitializeComponents()
        {
            _components.Values.ToList().ForEach(component => component.Initialize(this));
        }

        private void AddComponents()
        {
            GetComponentsInChildren<IEntityComponent>().ToList()
                .ForEach(component => _components.Add(component.GetType(), component));
        }

        public void DestroyEntity()
        {
            Destroy(gameObject);
        }
    }
}
