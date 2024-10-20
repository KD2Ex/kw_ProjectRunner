using UnityEngine;

public class UIBackToGame : UISelection
{
    [SerializeField] private CollectablesPanel panel;
    
    public override void Press()
    {
        base.Press();
        panel.gameObject.SetActive(false);
    }
}
