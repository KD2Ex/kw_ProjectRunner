using System.Collections;
using TMPro;
using UnityEngine;

public class ScorePanelDigits : ScorePanelElement
{
    [SerializeField] private FloatVariable coins;
    [SerializeField] private TMP_Text text;
    
    [SerializeField] private int rate;
    
    public override void Execute()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowScore());
    }

    public override void Stop()
    {
        
    }

    private IEnumerator ShowScore()
    {
        for (int i = 0; i < coins.Value; i += rate)
        {
            text.text = i.ToString();
            yield return null;
        }

        text.text = coins.Value.ToString();
    }

    public void Skip()
    {
        StopAllCoroutines();
        text.text = coins.Value.ToString();
    }
}