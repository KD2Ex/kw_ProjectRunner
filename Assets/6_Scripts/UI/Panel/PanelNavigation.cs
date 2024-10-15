using UnityEngine;

public abstract class PanelNavigation : MonoBehaviour
{
    protected UICoord currentPos;
    public UICoord CurrentPos => currentPos;
    protected UISelection[,] selections;
    
    protected virtual void Press()
    {
        Debug.Log(selections[currentPos.y, currentPos.x].name);
        selections[currentPos.y, currentPos.x].Press();
    }

    protected abstract void XMove(int value);
    protected abstract void YMove(int value);
    protected abstract void IsValid(UICoord coord);
}