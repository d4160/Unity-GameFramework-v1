using System.Collections;
using System.Collections.Generic;
using System.Linq;
using d4160.Core.Attributes;
using UnityEngine;
using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    public class InventoryItemBehaviour : MonoBehaviour
    {
        [Dropdown(ValuesProperty = "ItemNames")] [SerializeField]
        protected int _inventoryItem;

        public InventoryItemDefinition InventoryItem => InventoryManager.catalog.GetItemDefinitions()[_inventoryItem];

        public string ItemDefinitionId => InventoryItem.id;

        public int ItemDefinitionHash => InventoryItem.hash;

#if UNITY_EDITOR
        protected string[] ItemNames =>
            InventoryManager.catalog.GetItemDefinitions().Select(x => x.displayName).ToArray();
#endif
    }
}
