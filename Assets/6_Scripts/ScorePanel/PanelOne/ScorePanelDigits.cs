using System.Collections;
using System.Collections.Generic;
using _6_Scripts.Utils.DataFormating;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ScorePanelDigits : ScorePanelElement
{
    [SerializeField] private Coins coins;
    [FormerlySerializedAs("text")] [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text timerText;
    
    [SerializeField] private FloatVariable timerSeconds;
    [SerializeField] private FloatVariable timerMinutes;
    
    private int rate;

    private bool animationFinished;
    
    public override void Execute()
    {
        gameObject.SetActive(true);


        rate = coins.RunTotal == 0 ? 1 : Mathf.FloorToInt(Mathf.Log10(coins.RunTotal) + 1);
        Debug.Log($"rate: {rate}");
        StartCoroutine(ShowScore());
        StartCoroutine(ShowTime());
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

    private IEnumerator ShowTime()
    {
        var format = "";
        
        for (int i = 1; i <= timerSeconds.Value; i++)
        {
            format = DataFormating.FormatIntData(i, out var count);

            if (count == 1)
            {
                format = format.Insert(0, "<sprite=0>");
            }

            Debug.Log($"formatting timer: {count}, {format}");
            
            timerText.text = $"<sprite=0><sprite=0><sprite=10>{format}";
            yield return null;
        }

        //var seconds = FormatDigits((int)timerSeconds.Value, out var secondCount);
        
        for (int i = 0; i <= timerMinutes.Value; i++)
        {
            timerText.text = $"{DataFormating.FormatIntData(i)}<sprite=10>{format}";
            yield return null;
        }
    }
    
    private IEnumerator ShowScore()
    {
        for (int i = 0; i < coins.RunTotal; i += rate)
        {
            coinsText.text = DataFormating.FormatIntData(i);
            yield return null;
        }

        coinsText.text = DataFormating.FormatIntData(coins.RunTotal);
        animationFinished = true;
    }

    public void Skip()
    {
        StopAllCoroutines();
        coinsText.text = DataFormating.FormatIntData(coins.RunTotal);
        animationFinished = true;
    }
}