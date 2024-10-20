using UnityEngine;

public class UISettingsSelection : UISelection
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;
    
    public override void Press()
    {
        base.Press();
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }
}