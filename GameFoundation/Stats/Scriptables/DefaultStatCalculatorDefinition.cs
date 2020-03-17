using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game Framework/Game Data/Stat Calculators/DefaultStat Definition")]
    public class DefaultStatCalculatorDefinition : StatCalculatorDefinitionBase
    {
        public override float CalculateStat(int difficultyLevel = 1)
        {
            StatDefinition stat = Stat;
            InventoryItem item = Item;

            return StatManager.HasFloatValue(item, stat.idHash) ? item.GetStatFloat(stat.idHash) : item.GetStatInt(stat.idHash); ;
        }
    }
}