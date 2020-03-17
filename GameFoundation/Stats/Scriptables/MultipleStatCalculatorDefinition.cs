using d4160.Core;
using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Game Framework/Game Data/Stat Calculators/MultipleStat Definition")]
    public class MultipleStatCalculatorDefinition : MultipleStatCalculatorDefinitionBase
    {
        public override float[] CalculateStats(int difficultyLevel = 1, int additionalStats = 0)
        {
            float[] stats = new float[_itemStatPairs.Length + additionalStats];
            
            if (_useFirstStatForAllCalculations)
            {
                StatDefinition stat = GetStat(0);
                InventoryItem item = GetItem(0);
                float value = default;

                if (StatManager.HasFloatValue(item, stat.idHash))
                {
                    value = item.GetStatFloat(stat.idHash);
                }
                else
                {
                    value = item.GetStatInt(stat.idHash);
                }
                
                for (var i = 0; i < stats.Length; i++)
                {
                    if (i < _itemStatPairs.Length)
                    {
                        stats[i] = value;
                    }
                    else
                    {
                        stats[i] = 0;
                    }
                }
            }
            else
            {
                for (var i = 0; i < _itemStatPairs.Length + additionalStats; i++)
                {
                    if (i < _itemStatPairs.Length)
                    {
                        StatDefinition stat = GetStat(i);
                        InventoryItem item = GetItem(i);

                        stats[i] = StatManager.HasFloatValue(item, stat.idHash)
                            ? item.GetStatFloat(stat.idHash)
                            : item.GetStatInt(stat.idHash);
                        ;
                    }
                    else
                    {
                        stats[i] = 0;
                    }
                }
            }

            return stats;
        }

        public override float CalculateStat(int index, int difficultyLevel = 1)
        {
            int actIndex = _useFirstStatForAllCalculations ? 0 : index;
            StatDefinition stat = GetStat(actIndex);
            InventoryItem item = GetItem(actIndex);

            if (stat == null || item == null) return 0;

            return StatManager.HasFloatValue(item, stat.idHash) ? item.GetStatFloat(stat.idHash) : item.GetStatInt(stat.idHash);
        }

        public override void StoreStats(float[] values)
        {
            for (var i = 0; i < _itemStatPairs.Length; i++)
            {
                if (!values.IsValidIndex(i))
                {
                    continue;
                }

                StatDefinition stat = GetStat(i);
                InventoryItem item = GetItem(i);

                if(StatManager.HasFloatValue(item, stat.idHash))
                {
                    item.SetStatFloat(stat.idHash, values[i]);
                }
                else
                {
                    item.SetStatInt(stat.idHash, (int)values[i]);
                }
            }
        }

        public override void StoreStat(int index, float value)
        {
            StatDefinition stat = GetStat(index);
            InventoryItem item = GetItem(index);

            if (stat == null || item == null) return;

            if (StatManager.HasFloatValue(item, stat.idHash))
            {
                item.SetStatFloat(stat.idHash, value);
            }
            else
            {
                item.SetStatInt(stat.idHash, (int)value);
            }
        }
    }
}