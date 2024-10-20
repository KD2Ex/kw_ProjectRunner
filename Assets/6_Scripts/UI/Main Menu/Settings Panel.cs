using UnityEngine;

public class SettingsPanel : PanelNavigation
{
    [SerializeField] private UISettingSelection music;
    [SerializeField] private UISettingSelection soundFX;
    [SerializeField] private UISettingSelection brightness;
    [SerializeField] private UISettingSelection quality;
    [SerializeField] private UISettingSelection master;
    [SerializeField] private UIBackToMenuSelection back;

    private UISettingSelection currentSetting;

    private void Awake()
    {
        selections = new UISelection[,] {{music, soundFX, brightness}, {quality, master, back} };
    }

    // Start is called before the first frame update
    void Start()
    {
        selections[0, 0].Select(true);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Time.timeScale = 1f;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        SaveSystem.SaveSettings();
    }

    protected override void Press()
    {
        base.Press();

        if (currentSetting == null)
        {
            input.UIYMoveEvent -= YMove;
            input.UIXMoveEvent -= XMove;
            currentSetting = selections[currentPos.y, currentPos.x] as UISettingSelection;
        }
        else
        {
            input.UIYMoveEvent += YMove;
            input.UIXMoveEvent += XMove;
            currentSetting = null;
        }
    }

    protected override void XMove(int value)
    {
        var pos = new UICoord(currentPos.x + value, currentPos.y);

        
        if (pos.y == 1)
        {
            if (pos.x == -1)
            {
                pos = new UICoord(2, 0);
            }

            if (pos.x == 3)
            {
                pos = new UICoord(0, 0);
            }
        }
        else if (pos.x == 3)
        {
            pos = new UICoord(0, 1);
        }
        else
        {
            if (!IsValid(pos)) return;
        }
        
        selections[currentPos.y, currentPos.x].Select(false);
        currentPos = pos;
        selections[currentPos.y, currentPos.x].Select(true);
    }

    protected override void YMove(int value)
    {
        var pos = new UICoord(currentPos.x, currentPos.y + value);
        
        if (!IsValid(pos)) return;
        
        selections[currentPos.y, currentPos.x].Select(false);
        currentPos = pos;
        selections[currentPos.y, currentPos.x].Select(true);
    }

    protected override bool IsValid(UICoord coord)
    {
        return coord.x is >= 0 and < 3
               && coord.y is >= 0 and < 2;
    }
}
