using System;
using System.Collections.Generic;
using UnityEngine;

public class UISettingSelection : UISelection
{
    [SerializeField] private InputReader input;
    [SerializeField] private List<GameObject> bars;

    private int currentLevel = 1;
    private bool chosen;

    private void OnEnable()
    {
        ShowLevelBars();
    }

    public override void Press()
    {
        chosen = !chosen;
    }

    private void Update()
    {
        if (!chosen) return;
        
        switch (input.XMoveValue)
        {
            case > 0f:
                currentLevel++;
                if (!IsValid())
                {
                    currentLevel--;
                    break;
                }
                
                bars[currentLevel - 1].SetActive(true);
                break;
            case < 0f:
                currentLevel--;
                if (!IsValid())
                {
                    currentLevel++;
                    break;
                }
                
                bars[currentLevel - 1].SetActive(false);
                break;
        }
    }

    private void ShowLevelBars()
    {
        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].SetActive(i != currentLevel - 1);
        }
    }

    private bool IsValid()
    {
        return currentLevel is > 0 and < 6;
    }
}