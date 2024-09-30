using UnityEngine;

[CreateAssetMenu(fileName = "Timer Data", menuName = "Scriptable Objects/Timer Data")]
public class TimerData : ScriptableObject
{
    [field: SerializeField] public float ElapsedTime { get; set; }
    [SerializeField] private bool reset;

    private void OnEnable()
    {
        if (reset) ElapsedTime = 0f;
    }
}
