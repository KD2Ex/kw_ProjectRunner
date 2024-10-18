using UnityEngine;

public class UIBackToMenuSelection : UISelection
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;

    public override void Press()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }
}
