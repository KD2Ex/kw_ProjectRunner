using UnityEngine;

public class UIOpenNemoPanel : UISelection
{
    [SerializeField] private NemoGrownPanel panel;
    public override void Press()
    {
        base.Press();
        panel.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        CheckForUpgrade();
    }

    public override void Select(bool value)
    {
        if (CheckForUpgrade())
        {
            return;
        }
        
        base.Select(value);
    }

    private bool CheckForUpgrade()
    {
        if (GameManager.instance.NemoReadyToEvolve 
            && !GameManager.instance.NemoEvolvedOnLocation)
        {
            animator.SetBool(animSelect, true);
            return true;
        }

        return false;
    }
}
