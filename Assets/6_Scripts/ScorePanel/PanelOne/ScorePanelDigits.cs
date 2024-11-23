using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePanelDigits : ScorePanelElement
{
    [SerializeField] private Coins coins;
    [SerializeField] private TMP_Text text;
    
    [SerializeField] private int rate;

    private bool animationFinished;
    
    public override void Execute()
    {
        gameObject.SetActive(true);

        rate = Mathf.FloorToInt(Mathf.Log10(coins.RunTotal) + 1);
        Debug.Log($"rate: {rate}");
        StartCoroutine(ShowScore());
        StartCoroutine(WaitForAnimation());
    }

    public override void Stop()
    {
        
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitUntil(() => animationFinished);
        scorePanel.PlayNext();
    }

    private IEnumerator ShowScore()
    {
        for (int i = 0; i < coins.RunTotal; i += rate)
        {
            text.text = FormatDigits(i);
            yield return null;
        }

        text.text = FormatDigits((int)coins.RunTotal);
        animationFinished = true;
    }

    private string FormatDigits(int value)
    {
        string result = "";
        List<int> extractedNumbers = new();
        var count = value;

        while (count > 0)
        {
            var number = count % 10;
            extractedNumbers.Add(number);
            count /= 10;
        }

        extractedNumbers.Reverse();
        foreach (var number in extractedNumbers)
        {
            result += $"<sprite={number}>";
        }

        return result;
    }

    public void Skip()
    {
        StopAllCoroutines();
        text.text = FormatDigits((int)coins.RunTotal);
        animationFinished = true;
    }
}