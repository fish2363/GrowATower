using Assets._01.Member.CDH.Code.Events;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Assets._04.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO uiEventChannel;
        [SerializeField] private EventChannelSO turnManagerChannel;

        private void Awake()
        {
            turnManagerChannel.AddListener<DrawCardsStartEvent>(HandleDrawCardStart);
        }

        private void OnDestroy()
        {
            turnManagerChannel.RemoveListener<DrawCardsStartEvent>(HandleDrawCardStart);
        }

        private void HandleDrawCardStart(DrawCardsStartEvent evt)
        {
            
        }
    }
}
