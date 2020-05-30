using d4160.Core.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.GameFoundation;

public class ItemDefinitionsBehaviour : MonoBehaviour
{
    [Dropdown(ValuesProperty = "ItemNames")]
    [SerializeField]
    protected int[] _items;

    public InventoryItemDefinition ItemDefinition(int index) => InventoryManager.catalog.GetItemDefinitions()[_items[index]];

    public string ItemDefinitionId(int index) => ItemDefinition(index).id;

    public int ItemDefinitionHash(int index) => ItemDefinition(index).hash;

#if UNITY_EDITOR
    protected string[] ItemNames =>
        InventoryManager.catalog.GetItemDefinitions().Select(x => x.displayName).ToArray();
#endif
}
