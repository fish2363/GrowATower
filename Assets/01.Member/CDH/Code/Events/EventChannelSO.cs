using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._01.Member.CDH.Code.Events
{
    public class GameEvent
    {
        
    }

    [CreateAssetMenu(fileName = "EventChannel", menuName = "SO/EventChannel")]
    public class EventChannelSO : ScriptableObject
    {
        private Dictionary<Type, Action<GameEvent>> events = new();
        private Dictionary<Delegate, Action<GameEvent>> lookups = new();

        public void AddListener<T>(Action<T> handler) where T : GameEvent
        {
            if (lookups.ContainsKey(handler))
                return;

            Action<GameEvent> castHandler = (evt) => handler(evt as T);
            lookups[handler] = castHandler;

            Type evtType = typeof(T);
            if(events.ContainsKey(evtType))
            {
                events[evtType] += castHandler;
            }
            else
            {
                events[evtType] = castHandler;
            }
        }

        public void RemoveListener<T>(Action<T> handler) where T : GameEvent
        {
            if(lookups.TryGetValue(handler, out Action<GameEvent> action))
            {
                Type evtType = typeof(T);
                if(events.TryGetValue(evtType, out Action<GameEvent> castHandler))
                {
                    castHandler -= action;
                    if(castHandler == null)
                        events.Remove(evtType);
                    else
                        events[evtType] = castHandler;
                }
                lookups.Remove(handler);
            }
        }

        public void Invok(GameEvent evt)
        {
            if(events.TryGetValue(evt.GetType(), out Action<GameEvent> handler))
                handler?.Invoke(evt);
        }
    }
}