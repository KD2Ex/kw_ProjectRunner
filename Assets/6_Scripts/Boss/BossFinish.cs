using UnityEditor;
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
        SaveSystem.SaveCoins(); // save coins value
    }

    public void ExecuteDefeat()
    {
        DefeatBoss();
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BossFinish))]
public class BossFinishEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var targetScript = target as BossFinish;
        if (GUILayout.Button("DefeatBoss"))
        {
            targetScript?.ExecuteDefeat(); 
        }
        
        base.OnInspectorGUI();
    }
}
#endif