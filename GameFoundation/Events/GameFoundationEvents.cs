using d4160.GameFramework;
using UltEvents;
using UnityEngine;
using UnityEngine.GameFoundation;

public class GameFoundationEvents : MonoBehaviour
{
    [SerializeField] protected UltEvent _onInitializeCompleted;

    /* Using Start since a problem with FindObjectsOfType in DefaultDataLoader */
    protected virtual void Start()
    {
        if (!InventoryManager.IsInitialized)
            DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls += OnInitializeCompleted;
        else
            _onInitializeCompleted?.Invoke();
    }

    protected virtual void OnDestroy()
    {
        if (InventoryManager.IsInitialized)
            DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls -= OnInitializeCompleted;
    }

    protected void OnInitializeCompleted()
    {
        if (InventoryManager.IsInitialized)
        {
            _onInitializeCompleted?.Invoke();
        }
    }
}
