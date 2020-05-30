using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using d4160.Core.Attributes;
using d4160.GameFramework;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    public class InventoryController : MonoBehaviour
    {
        [Dropdown(ValuesProperty = "InventoryNames")]
        [SerializeField] protected int _inventory;

        [SerializeField] protected InventoryItemUltEvent _onItemAdded;
        [SerializeField] protected InventoryItemUltEvent _onItemRemoved;
        [SerializeField] protected InventoryItemUltEvent _onItemQuantityChanged;

#if UNITY_EDITOR
        protected string[] InventoryNames =>
            InventoryManager.catalog.GetCollectionDefinitions().Select(x => x.displayName).ToArray();
#endif

        public Inventory SelectedInventory { 
            get {
                if (InventoryManager.IsInitialized)
                {
                    return InventoryManager.GetInventories()[_inventory];
                }
                else
                {
                    return null;
                }
            } 
        }


        protected virtual void OnEnable()
        {
            RegisterCallbacks();
        }

        /* Using Start since a problem with FindObjectsOfType in DefaultDataLoader */
        protected virtual void Start()
        {
            if (!InventoryManager.IsInitialized)
                DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls += RegisterCallbacks;
        }

        protected virtual void OnDisable()
        {
            UnregisterCallbacks();
        }

        protected virtual void OnDestroy()
        {
            if (InventoryManager.IsInitialized)
                DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls -= RegisterCallbacks;
        }

        protected void RegisterCallbacks()
        {
            if (InventoryManager.IsInitialized)
            {
                Inventory inventory = SelectedInventory;

                inventory.onItemAdded += OnItemAdded;
                inventory.onItemRemoved += OnItemRemoved;
                inventory.onItemQuantityChanged += OnItemQuantityChanged;
            }
        }

        private void UnregisterCallbacks()
        {
            if (InventoryManager.IsInitialized)
            {
                Inventory inventory = SelectedInventory;

                inventory.onItemAdded -= OnItemAdded;
                inventory.onItemRemoved -= OnItemRemoved;
                inventory.onItemQuantityChanged -= OnItemQuantityChanged;
            }
        }

        protected virtual void OnItemAdded(InventoryItem item)
        {
            _onItemAdded?.Invoke(item);
        }

        protected virtual void OnItemRemoved(InventoryItem item)
        {
            _onItemRemoved?.Invoke(item);
        }

        protected virtual void OnItemQuantityChanged(InventoryItem item)
        {
            _onItemQuantityChanged?.Invoke(item);   
        }

        public virtual void AddItem(string itemDefinitionId, int quantity = 1)
        {
            SelectedInventory?.AddItem(itemDefinitionId, quantity);
        }

        public virtual void AddItem(int itemDefinitionHash, int quantity = 1)
        {
            SelectedInventory?.AddItem(itemDefinitionHash, quantity);
        }

        public virtual void RemoveItem(int itemDefinitionHash, int quantity = 1)
        {
            SelectedInventory?.RemoveItem(itemDefinitionHash, quantity);
        }

        public virtual void RemoveItem(string itemDefinitionId, int quantity = 1)
        {
            SelectedInventory?.RemoveItem(itemDefinitionId, quantity);
        }

        public virtual InventoryItem GetItem(int itemDefinitionHash)
        {
            return SelectedInventory?.GetItem(itemDefinitionHash);
        }

        public virtual int GetQuantity(int itemDefinitionHash)
        {
            if (InventoryManager.IsInitialized)
            {
                int quantity = 0;
                try
                {
                    quantity = SelectedInventory.GetQuantity(itemDefinitionHash);
                }
                catch
                {

                }

                return quantity;
            }
                
            return 0;
        }
    }

    [Serializable]
    public class InventoryItemUltEvent : UltEvent<InventoryItem>
    {
    }
}
