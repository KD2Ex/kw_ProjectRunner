using UnityEngine;
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
            currentElement.Stop();
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

    public void PlayNext()
    {
        Execute();
    }

    public void StartSequence()
    {
        input.DisableUIInput();
        input.DisableGameplayInput();
        input.DisableBossGameplayInput();
        
        Execute();
    }
}