namespace _01.Member.SD._01.Code.Unit.Interface
{
    public class CanGrowUnit:BaseUnit
    {
        public int currentLv = 1;
        public int maxLv;
        public override void OnTurnPassed()
        {
            if (currentLv >= maxLv)
                currentLv++;
            else
                return;
            
            
            
        }
    }
}