using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour<T> : ListBehaviour
{
    [SerializeField] protected T _item;

    public virtual T Item { get => _item; set => _item = value; }
}

public abstract class ItemBehaviour : MonoBehaviour
{ }