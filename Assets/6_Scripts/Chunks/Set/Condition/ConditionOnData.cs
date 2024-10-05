using UnityEngine;

public abstract class ConditionOnData : ScriptableObject
{
    public abstract bool Evaluate();
    public abstract bool Evaluate(float delta);
}