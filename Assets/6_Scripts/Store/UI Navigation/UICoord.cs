using System;
using UnityEngine;

[Serializable]
public struct UICoord
{
    [SerializeField] private int _x;
    [SerializeField] private int _y;

    public int x => _x;
    public int y => _y;
    
    public UICoord(int x, int y)
    {
        this._x = x;
        this._y = y;
    }

    public void MoveX()
    {
        _x += x;
        //_x = dir == Direction.DOWN ? _x - 1 : _x + 1;
    }

    public void MoveY()
    {
        _y = y;
        //_y = dir == Direction.UP ? _y - 1 : _y + 1;
    }

    public bool CoordEquals(UICoord coords)
    {
        try
        {
            if (coords.x == _x && coords.y == _y) return true;
        }
        catch (NullReferenceException ex)
        {
            Debug.Log("Null reference");
            return false;
        }
        return false;
    }
}
