using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ScorePanel : MonoBehaviour
{
    [SerializeField] private ScorePanelElement[] elements;
    [SerializeField] private InputReader input;
    [SerializeField] private AudioMixerGroup mixer;
    private ScorePanelElement currentElement;
    private int index;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
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
            EndSequence();
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
        input.CutsceneSkipEvent += Execute;
        
        input.DisableUIInput();
        input.DisableGameplayInput();
        input.DisableBossGameplayInput();
        
        GameManager.instance.SceneMusic.Source.Pause();
        mixer.audioMixer.SetFloat("soundFXVolume", -80f);
        
        Execute();
    }

    private void EndSequence()
    {
        input.EnableUIInput();
        input.EnableGameplayInput();
        input.EnableBossGameplayInput();
        
        input.CutsceneSkipEvent -= Execute;
    }
}