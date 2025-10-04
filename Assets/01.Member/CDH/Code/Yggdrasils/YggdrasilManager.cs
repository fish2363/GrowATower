using Assets._01.Member.CDH.Code.Events;
using UnityEngine;

namespace Assets._01.Member.CDH.Code.Yggdrasils
{
    public class YggdrasilManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO gameEventChannel;
        [SerializeField] private int maxHealth;

        private Yggdrasil yggdrasil;

        private void Awake()
        {
            Yggdrasil.Instance.Initialize(maxHealth);
            Yggdrasil.Instance.OnYggdrasilHealthChanged += HandleYggdrasilHealthChaned;
        }
        private void OnDestroy()
        {
            Yggdrasil.Instance.OnYggdrasilHealthChanged -= HandleYggdrasilHealthChaned;
        }

        private void HandleYggdrasilHealthChaned(int health)
        {
            if (health <= 0)
            {
                gameEventChannel.Invoke(GameEvents.GameOverEvent.Initializer());
            }
        }
    }
}
