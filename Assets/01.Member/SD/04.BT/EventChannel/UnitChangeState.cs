using _01.Member.SD._01.Code.Unit.Interface.UnitSupport;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/UnitChangeState")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "UnitChangeState", message: "change state to [newValue]", category: "Events",
    id: "585061aaecf56ca3bf4d7ccaab00c614")]
public sealed partial class UnitChangeState : EventChannel<UnitState>
{
    public delegate void StateChangeEventHandler(UnitState newValue);
    public event StateChangeEventHandler Event; 

    public void SendEventMessage(UnitState newValue)
    {
        Event?.Invoke(newValue);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<UnitState> newValueBlackboardVariable = messageData[0] as BlackboardVariable<UnitState>;
        var newValue = newValueBlackboardVariable != null ? newValueBlackboardVariable.Value : default(UnitState);

        Event?.Invoke(newValue);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        StateChangeEventHandler del = (newValue) =>
        {
            BlackboardVariable<UnitState> var0 = vars[0] as BlackboardVariable<UnitState>;
            if(var0 != null)
                var0.Value = newValue;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as StateChangeEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as StateChangeEventHandler;
    }
}

