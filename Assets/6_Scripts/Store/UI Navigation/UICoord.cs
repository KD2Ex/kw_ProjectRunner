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
