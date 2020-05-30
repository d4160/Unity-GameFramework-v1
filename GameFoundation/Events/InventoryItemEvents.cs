using d4160.GameFoundation;
using UnityEngine;
using UnityEngine.GameFoundation;

public class InventoryItemEvents : InventoryItemBehaviour
{
    [SerializeField] protected InventoryItemUltEvent _onValidated;

    protected InventoryItem _lastSelectedItem;

    public InventoryItem LastSelectedItem => _lastSelectedItem;
    public int LastItemQuantity => _lastSelectedItem.quantity;

    public void Invoke(InventoryItem item)
    {
        if (item.hash == ItemDefinitionHash)
        { 
            _lastSelectedItem = item;

            _onValidated?.Invoke(item);
        }
    }
}
