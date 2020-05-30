using d4160.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectContainer : ListBehaviour<GameObjectItem, GameObject>
{
    public void SetActive(bool active, int idx = 0)
    {
        if (_items.IsValidIndex(idx))
        {
            this[idx].Item.SetActive(active);
        }
    }

    public void SetActive(bool active, bool others, int idx = 0)
    {
        if (_items.IsValidIndex(idx))
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (idx == i)
                {
                    this[i].Item.SetActive(active);
                }
                else
                {
                    this[i].Item.SetActive(others);
                }
            }
        }
    }

    public void SetActive(bool active, bool others, params int[] indexes)
    {
        for (int k = 0; k < indexes.Length; k++)
        {
            if (_items.IsValidIndex(indexes[k]))
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (indexes[k] == i)
                    {
                        this[i].Item.SetActive(active);
                    }
                    else
                    {
                        this[i].Item.SetActive(others);
                    }
                }
            }
        }
    }

    public void SetActive(bool active, params int[] indexes)
    {
        for (int k = 0; k < indexes.Length; k++)
        {
            if (_items.IsValidIndex(indexes[k]))
            {
                this[indexes[k]].Item.SetActive(active);
            }
        }
    }

    public void SetActive(bool active, int quantity, int start = 0)
    {
        for (int k = start; k < quantity; k++)
        {
            if (_items.IsValidIndex(k))
            {
                this[k].Item.SetActive(active);
            }
        }
    }

    public void SetActive(bool active, int quantity, bool others, int start = 0)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (i >= start && i < quantity + start)
            {
                this[i].Item.SetActive(active);
            }
            else
            {
                this[i].Item.SetActive(others);
            }
        }
    }

    public void SetActiveAll(bool active)
    {
        for (int k = 0; k < _items.Count; k++)
        {
            this[k].Item.SetActive(active);
        }
    }
}
