using System.Linq;
using d4160.Core.Attributes;
using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    using UnityEngine;

    public abstract class StatCalculatorDefinitionBase : ScriptableObject, IStatCalculator
    {
        [SerializeField]
        private ItemStatPair _itemStatPair;

#if UNITY_EDITOR
        protected string[] StatNames => StatManager.catalog.GetStatDefinitions().Select((x) => x.displayName).ToArray();
        protected string[] ItemNames => InventoryManager.catalog.GetItemDefinitions().Select((x) => x.displayName).ToArray();
#endif

        public StatDefinition Stat => StatManager.catalog.GetStatDefinitions()[_itemStatPair.stat];
        public InventoryItem Item => UnityEngine.GameFoundation.Inventory.main.GetItem(InventoryManager.catalog.GetItemDefinitions()[_itemStatPair.item]);

        public abstract float CalculateStat(int difficultyLevel = 1);
    }

    public abstract class StatCalculatorDefinitionBase<T> : ScriptableObject, IStatCalculator<T>
    {
        public abstract T CalculateStat(int difficultyLevel = 1);
    }
}