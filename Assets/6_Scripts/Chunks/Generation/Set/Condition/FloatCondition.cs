using UnityEngine;

[CreateAssetMenu(fileName = "Every X times", menuName = "Scriptable Objects/Chunk/Conditions/Repeatable")]
public class FloatCondition : ConditionOnData
{
    [SerializeField] private float after;
    //[SerializeField] private FloatVariable data;

    public float counter = 0;
    public bool DebugEvalResult;
    
    public void Initialize()
    {
        
    }
    
    public override bool Evaluate()
    {
        counter++;

        DebugEvalResult = Compare();
        return DebugEvalResult;
    }

    public override bool Evaluate(float delta)
    {
        counter += delta;
        return Compare();
    }

    private bool Compare()
    {
        if (counter >= after)
        {
            counter = 0;
            return true;
        }
        
        return false;
    }
}