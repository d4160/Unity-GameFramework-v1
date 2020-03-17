using System.Linq;
using d4160.Core;
using d4160.Core.Attributes;
using Malee;
using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    using UnityEngine;

    public abstract class MultipleStatCalculatorDefinitionBase : ScriptableObject, IMultipleStatCalculator
    {
        [SerializeField] protected bool _useFirstStatForAllCalculations;
        [SerializeField] [Reorderable(pageSize = 10, paginate = true)]
        protected ItemStatPairReorderableList _itemStatPairs;

#if UNITY_EDITOR
        protected string[] StatNames => StatManager.catalog.GetStatDefinitions().Select((x) => x.displayName).ToArray();
        protected string[] ItemNames => InventoryManager.catalog.GetItemDefinitions().Select((x) => x.displayName).ToArray();
#endif

        public StatDefinition GetStat(int index) => _itemStatPairs.IsValidIndex(index) ? StatManager.catalog.GetStatDefinitions()[_itemStatPairs[index].stat] : null;
        public InventoryItem GetItem(int index) => _itemStatPairs.IsValidIndex(index) ? Inventory.main.GetItem(InventoryManager.catalog.GetItemDefinitions()[_itemStatPairs[index].item]) : null;

        public abstract float[] CalculateStats(int difficultyLevel = 1, int additionalStats = 0);
        public abstract float CalculateStat(int index, int difficultyLevel = 1);

        public abstract void StoreStats(float[] values);

        public abstract void StoreStat(int index, float value);
    }

    [System.Serializable]
    public class ItemStatPairReorderableList : ReorderableArray<ItemStatPair>
    {
    }
}