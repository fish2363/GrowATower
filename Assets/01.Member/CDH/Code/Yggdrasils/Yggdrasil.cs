using Assets._04.Core;
using UnityEngine;

namespace Assets._01.Member.CDH.Code.Yggdrasils
{
    public delegate void OnYggdrasilHealthChanged(int health);

    public class Yggdrasil : Singleton<Yggdrasil>
    {
        private int maxHealth;
        private int health;

        public OnYggdrasilHealthChanged OnYggdrasilHealthChanged;

        public void Initialize(int maxHealth)
        {
            health = maxHealth;
        }

        public void Hit(int damage)
        {
            health -= damage;
            OnYggdrasilHealthChanged?.Invoke(health);
        }
    }
}
