using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListBehaviour<T1, T2> : ListBehaviour where T1 : ItemBehaviour<T2> 
{
    [SerializeField] protected List<T1> _items;

    public virtual T1 this[int idx]
    {
        get
        {
            return _items[idx];
        }
        set => _items[idx] = value;
    }
}

public abstract class ListBehaviour : MonoBehaviour
{ }
