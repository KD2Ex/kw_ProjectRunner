﻿using System.Collections;
using UnityEngine;

public class NemoGrownPanel : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private GameObject[] levels;

    private int currentIndex => GameManager.instance.NemoCurrentLevel;
    private GameObject currentLevel => levels[currentIndex];

    private bool canBeUpgraded => 
        GameManager.instance.NemoReadyToEvolve &&
        !GameManager.instance.NemoEvolvedOnLocation;
    
    private void OnEnable()
    {
        Open();

        StartCoroutine(Wait());
        IEnumerator Wait()
        {
            yield return null;
            yield return null;
            input.CutsceneSkipEvent += Close;
        }
    }

    private void OnDisable()
    {
        input.CutsceneSkipEvent -= Close;
    }   

    private void Open()
    {/*
        foreach (var level in levels)
        {
            level.SetActive(false);
        }
        */

        if (canBeUpgraded)
        {
            upgradeButton.SetActive(true);
        }
        input.DisableUIInput();
        Debug.Log("open");
        currentLevel.SetActive(true);
    }
    
    private void Close()
    {
        if (canBeUpgraded)
        {
            GameManager.instance.NemoEvolvedOnLocation = true;
            GameManager.instance.NemoCurrentLevel++;
            
            currentLevel.SetActive(true);
            levels[currentIndex - 1].SetActive(false);
            
            upgradeButton.SetActive(false);
            return;
        }
        
        Debug.Log("close");
        input.EnableUIInput();
        currentLevel.SetActive(false);
        gameObject.SetActive(false);
    }
}