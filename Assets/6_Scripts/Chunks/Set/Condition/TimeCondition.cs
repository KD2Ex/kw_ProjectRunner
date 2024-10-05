using UnityEngine;

[CreateAssetMenu(fileName = "Time Condition", menuName = "Scriptable Objects/Chunks/Conditions/After time")]
public class TimeCondition : ScriptableCondition
{
    [SerializeField] private float TimeToEvaluate;
    [SerializeField] private FloatVariable timerData;

    public override bool Evaluate() => timerData.Value > TimeToEvaluate;

    public override void ResetTrigger()
    {
    }
}