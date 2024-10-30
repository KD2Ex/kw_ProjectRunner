using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WithinTimeCondition : ChunkSpawnCondition
{
    private WithinTimeConditionData data;
    
    private int counter = 0;
    private float currentP = 0f;

    private bool evaluated;

    private float lastProb = 0f;
    
    public WithinTimeCondition(WithinTimeConditionData data)
    {
        this.data = data;
        
    }

    public override bool Evaluate()
    {
        
        float floatTime = data.Time;
        
        currentP = (100 / floatTime) * (data.Timer.Value % (floatTime + 1));
        Debug.Log($"Probability: {currentP}");
        Debug.Log($"Count: {counter}");
        
        if (counter == data.Count)
        {
            if (currentP < lastProb)
            {
                counter = 0;
            }
            return false;
        }
        
        var intWeight = Convert.ToInt32(currentP * 10);
        var randValue = Random.Range(0, 1001);

        Debug.Log($"Weight: {intWeight} Rand: {randValue}");
        
        var result = randValue <= intWeight;

        if (result)
        {
            if (evaluated) return false;
            
            lastProb = currentP;
            evaluated = true;
            counter++;
        }
        
        return result;
    }

    public override void ResetTrigger()
    {
        evaluated = false;

    }
}