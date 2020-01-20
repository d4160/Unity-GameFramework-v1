namespace d4160.Systems.Flow
{
    public class LevelCalculator : StatCalculatorBase
    {
        public virtual void LevelTo(int level)
        {
            IntStat = level;

            /* Can add limits or something like that, override property */
        }

        public virtual void LevelUp(int amount = 1)
        {
            IntStat += amount;
        }

        public virtual void LevelDown(int amount = 1)
        {
            IntStat -= amount;
        }
    }
}