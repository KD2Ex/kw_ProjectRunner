using UnityEngine;

public class UIBackToGame : UISelection
{
    [SerializeField] private CollectablesPanel panel;
    
    public override void Press()
    {
        panel.gameObject.SetActive(false);
    }
}
