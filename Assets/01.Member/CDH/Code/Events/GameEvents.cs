namespace Assets._01.Member.CDH.Code.Events
{
    public static class GameEvents
    {
        public static readonly GameOverEvent GameOverEvent = new();
    }

    /// <summary>
    /// to GameManager
    /// </summary>
    public class GameOverEvent : GameEvent
    {
        public GameOverEvent Initializer()
        {
            return this;
        }
    }
}
