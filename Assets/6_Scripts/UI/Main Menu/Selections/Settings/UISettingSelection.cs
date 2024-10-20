using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingSelection : UISelection
{
    [SerializeField] private InputReader input;
    [SerializeField] private List<GameObject> bars;
    [SerializeField] private ColorData selectionColor;
    
    [SerializeField] private bool disablePrevBars;

    private Setting setting;
    
    private Image image;
    private Color origColor;
    
    private int currentLevel = 1;
    private bool chosen;
    
    protected override void Awake()
    {
        base.Awake();
        setting = GetComponent<Setting>();
        image = GetComponent<Image>();
        origColor = image.color;
    }

    private void OnEnable()
    {
        setting?.LoadLevelValue(ref currentLevel);
        Debug.Log(currentLevel);
        
    }

    private void Start()
    {
        ShowLevelBars();
    }

    public override void Press()
    {
        chosen = !chosen;

        if (chosen)
        {
            input.UIXMoveEvent += ChangeLevel;
            image.color = selectionColor.color;
        }
        else
        {
            input.UIXMoveEvent -= ChangeLevel;
            image.color = origColor;
        }

        Debug.Log(currentLevel);
    }
    
    
    private void ChangeLevel(int value)
    {
        switch (value)
        {
            case > 0:
                currentLevel++;
                if (!IsValid())
                {
                    currentLevel--;
                    break;
                }
                
                bars[currentLevel - 1].SetActive(true);
                if (disablePrevBars) bars[currentLevel - 2].SetActive(false);
                break;
            case < 0:
                currentLevel--;
                if (!IsValid())
                {
                    currentLevel++;
                    break;
                }
                
                bars[currentLevel].SetActive(false);
                if (disablePrevBars) bars[currentLevel - 1].SetActive(true);
                break;
        }
        
        setting.SetLevel(currentLevel);
        
    }
    
    private void Update()
    {
        if (!chosen) return;
        
        
    }

    private void ShowLevelBars()
    {
        Debug.Log(currentLevel);
        
        if (disablePrevBars)
        {
            for (int i = 0; i < bars.Count; i++)
            {
                bars[i].SetActive(false);
            }
            
            bars[currentLevel - 1].SetActive(true);
            return;
        }
        
        for (int i = 0; i < bars.Count; i++)
        {
            Debug.Log(i < currentLevel);
            bars[i].SetActive(i < currentLevel);
        }
    }

    private bool IsValid()
    {
        return currentLevel > 0 && currentLevel < bars.Count + 1;
    }
}