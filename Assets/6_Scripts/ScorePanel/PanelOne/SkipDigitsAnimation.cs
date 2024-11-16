using UnityEngine;

public class SkipDigitsAnimation : ScorePanelElement
{
    [SerializeField] private ScorePanelDigits digits;
    
    public override void Execute()
    {
        digits.Skip();
    }

    public override void Stop()
    {
        digits.gameObject.SetActive(false);
    }
}
