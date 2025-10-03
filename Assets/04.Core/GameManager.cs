using Assets._01.Member.CDH.Code.Events;
using Assets._01.Member.CDH.Code.Yggdrasils;
using UnityEngine;

namespace Assets._04.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO uiEventChannel;
        [SerializeField] private EventChannelSO turnManagerChannel;
        [SerializeField] private EventChannelSO gameEventChannel;
        [SerializeField] private TESTSOList testList;
        [SerializeField] private int maxHealth;

        private Yggdrasil yggdrasil;

        private void Awake()
        {
            turnManagerChannel.AddListener<DrawCardsStartEvent>(HandleDrawCardStart);

            Yggdrasil.Instance.Initialize(maxHealth);
            Yggdrasil.Instance.OnYggdrasilHealthChanged += HandleYggdrasilHealthChaned;
        }

        private void OnDestroy()
        {
            turnManagerChannel.RemoveListener<DrawCardsStartEvent>(HandleDrawCardStart);

            Yggdrasil.Instance.OnYggdrasilHealthChanged -= HandleYggdrasilHealthChaned;
        }

        private void HandleYggdrasilHealthChaned(int health)
        {
            if (health <= 0)
            {
                gameEventChannel.Invoke(GameEvents.GameOverEvent.Initializer());
            }
        }

        private void HandleDrawCardStart(DrawCardsStartEvent evt)
        {
            var drawCardsEvt = UIEvents.randomShuffle;
            drawCardsEvt.chooseTimer = 10f;
            drawCardsEvt.resultList = RandomShuffle.ShuffleRandomCards(testList.tests, 6);
            uiEventChannel.Invoke(drawCardsEvt);
        }
    }
}
