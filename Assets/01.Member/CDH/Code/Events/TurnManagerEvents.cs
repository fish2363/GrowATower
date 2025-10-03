namespace Assets._01.Member.CDH.Code.Events
{
    public static class TurnManagerEvents
    {
        public static readonly WaveStartEvent WaveStartEvent = new();
        public static readonly WaveEndEvent WaveEndEvent = new();
        public static readonly WaveClearTimeEvent WaveClearTimeEvent = new();
        public static readonly DrawCardsStartEvent DrawCardsStartEvent = new();
        public static readonly DrawCardsEndEvent EndDrawCardsEvent = new();
        public static readonly WaitingTimeStartEvent WaitingTimeStartEvent = new();
        public static readonly WaitingTimeEndEvent WaitingTimeEndEvent = new();
        public static readonly WaitingTimeSkipEvent WaitingTimeSkipEvent = new();
        public static readonly BreakTimeSkipEvent BreakTimeSkipEvent = new();
        public static readonly BreakTimeStartEvent BreakTimeStartEvent = new();
        public static readonly BreakTimeEndEvent BreakTimeEndEvent = new();
    }

    public class WaveClearTimeEvent : GameEvent
    {
        public float waveClearTime;

        public WaveClearTimeEvent Initalizer(float waveClearTime)
        {
            this.waveClearTime = waveClearTime;
            return this;
        }
    }
    public class WaveStartEvent : GameEvent
    {
        public WaveStartEvent Initalizer()
        {
            return this;
        }

    }
    /// <summary>
    /// To TurnManager Event
    /// </summary>
    public class WaveEndEvent : GameEvent
    {
        public WaveEndEvent Initalizer()
        {
            return this;
        }
    }
    public class DrawCardsStartEvent : GameEvent
    {
        public DrawCardsStartEvent Initalizer()
        {
            return this;
        }

    }
    /// <summary>
    /// To TurnManager Event
    /// </summary>
    public class DrawCardsEndEvent : GameEvent
    {

        public DrawCardsEndEvent Initalizer()
        {
            return this;
        }
    }
    public class WaitingTimeStartEvent : GameEvent
    {

        public WaitingTimeStartEvent Initalizer()
        {
            return this;
        }
    }
    public class WaitingTimeEndEvent : GameEvent
    {
        public WaitingTimeEndEvent Initalizer()
        {
            return this;
        }
    }
    /// <summary>
    /// To TurnManager Event
    /// </summary>
    public class WaitingTimeSkipEvent : GameEvent
    {
        public WaitingTimeSkipEvent Initalizer()
        {
            return this;
        }

    }
    public class BreakTimeStartEvent : GameEvent
    {
        public BreakTimeStartEvent Initalizer()
        {
            return this;
        }
    }
    /// <summary>
    /// To TurnManager Event
    /// </summary>
    public class BreakTimeSkipEvent : GameEvent
    {
        public BreakTimeSkipEvent Initalizer()
        {
            return this;
        }
    }
    public class BreakTimeEndEvent : GameEvent
    {
        public BreakTimeEndEvent Initalizer()
        {
            return this;
        }
    }
}
