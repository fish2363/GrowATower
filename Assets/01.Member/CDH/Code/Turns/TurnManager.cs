using Assets._01.Member.CDH.Code.Events;
using Assets._04.Core;
using UnityEngine;

namespace Assets._01.Member.CDH.Code.Turns
{
    public class TurnManager : MonoSingleton<TurnManager>
    {
        [SerializeField] private EventChannelSO turnManagerEventChannel;
        [SerializeField] private float waitingTime;
        [SerializeField] private float breakTime;

        private bool isWave;
        private bool isWaitingTime;
        private bool isBreakTime;
        private float timer;

        private void Awake()
        {
            turnManagerEventChannel.AddListener<WaveEndEvent>(HandleWaveEnd);
            turnManagerEventChannel.AddListener<DrawCardsEndEvent>(HandleDrawCardsEnds);
            turnManagerEventChannel.AddListener<WaitingTimeSkipEvent>(HandleWaitingTimeSkip);
            turnManagerEventChannel.AddListener<BreakTimeSkipEvent>(HandleBreakTimeSkip);

            StartWave();
        }

        private void OnDestroy()
        {
            turnManagerEventChannel.RemoveListener<WaveEndEvent>(HandleWaveEnd);
            turnManagerEventChannel.RemoveListener<DrawCardsEndEvent>(HandleDrawCardsEnds);
            turnManagerEventChannel.RemoveListener<WaitingTimeSkipEvent>(HandleWaitingTimeSkip);
            turnManagerEventChannel.RemoveListener<BreakTimeSkipEvent>(HandleBreakTimeSkip);
        }

        private void Update()
        {
            if(isWaitingTime || isBreakTime || isWave)
            {
                timer += Time.deltaTime;
            }
            if(isWaitingTime)
            {
                if(timer >= waitingTime)
                {
                    StartWave();
                }
            }
            if(isBreakTime)
            {
                if(timer >= breakTime)
                {
                    DrawCards();
                }
            }
        }

        private void HandleWaveEnd(WaveEndEvent evt)
        {
            // 휴식시간 시작
            Initialize();
            isBreakTime = true;
            turnManagerEventChannel.Invoke(TurnManagerEvents.BreakTimeStartEvent.Initalizer());
            turnManagerEventChannel.Invoke(TurnManagerEvents.WaveClearTimeEvent.Initalizer(timer));
        }

        private void HandleDrawCardsEnds(DrawCardsEndEvent evt)
        {
            // 대기 시간 시작
            Initialize();
            isWaitingTime = true;
            turnManagerEventChannel.Invoke(TurnManagerEvents.WaitingTimeStartEvent.Initalizer());
        }

        private void HandleWaitingTimeSkip(WaitingTimeSkipEvent evt)
        {
            // 바로 웨이브 시작
            StartWave();
        }

        private void HandleBreakTimeSkip(BreakTimeSkipEvent evt)
        {
            // 바로 카드 뽑기 시작
            DrawCards();
        }

        private void StartWave()
        {
            Initialize();
            isWave = true;
            turnManagerEventChannel.Invoke(TurnManagerEvents.WaveStartEvent.Initalizer());
            turnManagerEventChannel.Invoke(TurnManagerEvents.WaitingTimeEndEvent.Initalizer());
        }

        private void DrawCards()
        {
            Initialize();
            turnManagerEventChannel.Invoke(TurnManagerEvents.DrawCardsStartEvent.Initalizer());
            turnManagerEventChannel.Invoke(TurnManagerEvents.BreakTimeEndEvent.Initalizer());
        }

        private void Initialize()
        {
            isBreakTime = false;
            isWave = false;
            isWaitingTime = false;
            timer = 0.0f;
        }
    }
}
