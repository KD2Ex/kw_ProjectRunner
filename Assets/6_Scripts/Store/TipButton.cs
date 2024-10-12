using UnityEngine;

public class TipButton : MonoBehaviour
{
    [SerializeField] private StorePanel storePanel;
    [SerializeField] private UICoord coords;
    [SerializeField] private GameObject button;
    
    void Update()
    {
        button.SetActive(storePanel.CurrentPos.CoordEquals(coords));
    }
    
}
