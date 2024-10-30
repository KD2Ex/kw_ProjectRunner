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

        var inFirstMinute = 0;
        var beforePity = 0;
        var AtPity = 0;
        
        for (int i = 0; i < 1000; i++)
        {
            var timings = Test(i);

            foreach (var timing in timings)
            {
                if (timing < 60)
                {
                    inFirstMinute++;
                    continue;
                }

                if (timing < 90)
                {
                    beforePity++;
                    continue;
                }

                AtPity++;
            }
        }

        if (AtPity == 0) AtPity = 1;
        
        var withoutPity = inFirstMinute + beforePity;
        
        Debug.Log($"In first minute: {inFirstMinute} Before Pity: {beforePity} With Pity: {AtPity}");
        Debug.Log($"Without Pity: {withoutPity}, Probability: {(float)withoutPity / (float)AtPity}");
    }

    private int[] Test(int index)
    {
        Debug.Log($"Test number: {index}");

        int[] timings = new int[2];
        
        var count = 0;        
        for (int i = 0; i < 120; i += 3)
        {
            var res = EvaluateAtTime(i);

            if (res)
            {
                timings[count] = i;
                count++;
                Debug.Log($"True at time {i}");
            }
            
            if (count == 2) break;
        }

        return timings;
    }

    public override bool Evaluate()
    {
        float floatTime = data.Time;

        var currentTime = data.Timer.Value % (floatTime + 1);
        currentP = (100 / floatTime) * currentTime;
        //Debug.Log($"Probability: {currentP}");
        //Debug.Log($"Count: {counter}");

        if (counter == data.Count)
        {
            if (currentP < lastProb)
            {
                counter = 0;
            }
            return false;
        }

        var intWeight = 8;//Convert.ToInt32(currentP * 10);

        if (currentTime > 90)
        {
            intWeight += Convert.ToInt32((currentTime - 90) * 33);
        }
        
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

    private bool EvaluateAtTime(float time)
    {
        var intWeight = 25;//Convert.ToInt32(currentP * 10);

        if (time > 90)
        {
            intWeight += Convert.ToInt32((time - 90) * 33);
        }
        
        var randValue = Random.Range(0, 1001);
        
        var result = randValue <= intWeight;
        return result;
    }
}