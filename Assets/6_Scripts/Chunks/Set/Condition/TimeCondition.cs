using UnityEngine;

[CreateAssetMenu(fileName = "Time Condition", menuName = "Scriptable Objects/Chunks/Spawn Condition")]
public class TimeCondition : ScriptableCondition
{
    [SerializeField] private float TimeToEvaluate;
    [SerializeField] private FloatVariable timerData;

    public override bool Evaluate() => timerData.Value > TimeToEvaluate;
}