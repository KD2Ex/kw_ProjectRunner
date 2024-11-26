using UnityEngine;
using UnityEngine.Events;

public class BossFinish : MonoBehaviour
{
    [SerializeField] private AudioClip bossSound;
    [SerializeField] private AudioSource source;


    public UnityEvent OnBossDefeated;
    
    private void Update()
    {
        if (source.time >= bossSound.length)
        {
            //change level
            DefeatBoss();
            gameObject.SetActive(false);
        }
    }

    private void DefeatBoss()
    {
        OnBossDefeated?.Invoke();
        Time.timeScale = 0;
    }
}
