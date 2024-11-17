using System;
using System.Collections;
using UnityEngine;

public class UIOpenNemoPanel : UISelection
{
    [SerializeField] private NemoGrownPanel panel;
    [SerializeField] private InputReader input;

    private bool pressed;
    
    public override void Press()
    {
        base.Press();
        panel.gameObject.SetActive(true);

        Debug.Log("nemo press");
        pressed = false;

    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    private void CloseNemo()
    {
        if (!pressed) return;
        if (!panel.gameObject.activeInHierarchy) return;
        
        panel.gameObject.SetActive(false);
    }
}
