using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingSelection : UISelection
{
    [SerializeField] private InputReader input;
    [SerializeField] private List<GameObject> bars;
    [SerializeField] private ColorData selectionColor;

    private Image image;
    private Color origColor;
    
    private int currentLevel = 1;
    private bool chosen;

    protected override void Awake()
    {
        base.Awake();

        image = GetComponent<Image>();
        origColor = image.color;
    }

    private void OnEnable()
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
                break;
            case < 0:
                currentLevel--;
                if (!IsValid())
                {
                    currentLevel++;
                    break;
                }
                
                bars[currentLevel].SetActive(false);
                break;
        }
    }
    
    private void Update()
    {
        if (!chosen) return;
        
        
    }

    private void ShowLevelBars()
    {
        for (int i = 0; i < bars.Count; i++)
        {
            bars[i].SetActive(i < currentLevel);
        }
    }

    private bool IsValid()
    {
        return currentLevel is > 0 and < 6;
    }
}