using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private ScorePanelElement[] elements;
    [SerializeField] private InputReader input;

    private ScorePanelElement currentElement;
    private int index;

    private void OnEnable()
    {
        input.CutsceneSkipEvent += Execute;
    }

    private void OnDisable()
    {
        input.CutsceneSkipEvent -= Execute;
    }
    
    private void Execute()
    {
        if (currentElement)
        {
            currentElement.gameObject.SetActive(false);
        }
        
        if (index == elements.Length)
        {
            index = 0;
            SceneManager.LoadSceneAsync("Main"); // load curr location; save ?
            //SaveSystem.Save();
            return;
        }
        
        var elem = elements[index];

        if (!elem.gameObject.activeInHierarchy)
        {
            elem.gameObject.SetActive(true);            
        }
        
        elem.Execute();
        index++;
        currentElement = elem;
    }

    public void StartSequence()
    {
        input.DisableUIInput();
        input.DisableGameplayInput();
        input.DisableBossGameplayInput();
        
        Execute();
    }
}