using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private bool _limitWithCapacity;
    [SerializeField] private Vector3Int _capacity;

    public Vector3 GetCellTo(ref Vector3Int cell, Space space)
    {
        if (_limitWithCapacity)
        {
            var div = _capacity.x / 2;
            var mod = _capacity.x % 2;

            cell.x = _capacity.x <= 1 ? 0 : Mathf.Clamp(cell.x, -div, (div - 1 + mod));

            div = _capacity.y / 2;
            mod = _capacity.y % 2;

            cell.y = _capacity.y <= 1 ? 0 : Mathf.Clamp(cell.y, -(div - 1 + mod), div);

            div = _capacity.z / 2;
            mod = _capacity.z % 2;

            cell.z = _capacity.z <= 1 ? 0 : Mathf.Clamp(cell.z, -(div - 1 + mod), div);
        }
        
        switch (space)
        {
            case Space.World:
                return _grid.CellToWorld(new Vector3Int(cell.x, cell.y, cell.z));
            case Space.Self:
                return _grid.CellToLocal(new Vector3Int(cell.x, cell.y, cell.z));
            default:
                return default;
        }
    }

    public Vector3Int GetCellFromPosition(Vector3 position, Space space)
    {
        switch (space)
        {
            case Space.World:
                return _grid.WorldToCell(position);
            case Space.Self:
                return _grid.LocalToCell(position);
            default:
                return default;
        }
    }
}
