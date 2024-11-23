using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpeedSign : MonoBehaviour
{
    [SerializeField] private FloatVariable speed;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private AudioSource source;
    [SerializeField] private SoundList clips;
    
    void Start()
    {

    }
    
    public void TriggerSave()
    {
        UpdateSpeed();
        PlaySound();
    }

    private void PlaySound()
    {
        source.clip = clips.GetRandom();
        source.Play();
    }
    
    private void UpdateSpeed()
    {
        var text = MatchSpeedValue(speed.Value).ToString();
        UpdateUI(text);
    }

    private int MatchSpeedValue(float speed)
    {
        if (speed < 5f) return 15;
        if (speed < 10f) return Random.Range(20, 30);
        if (speed < 15f) return Random.Range(30, 40);
        if (speed < 20f) return Random.Range(40, 50);
        if (speed < 25f) return Random.Range(50, 60);
        if (speed < 30f) return Random.Range(70, 90);

        return 99;
    }

    private void UpdateUI(string text)
    {
        textComponent.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            textComponent.text += $"<sprite={text[i]}>";
        }
    }
}
