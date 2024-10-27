using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PooledRandom
{
    private int size;
    private Stack<int> values;
    private int min;
    private int max;

    private int line = 6;
    public PooledRandom(int size, int min, int max)
    {
        this.size = size;
        this.min = min;
        this.max = max;

        values = new(size);

        InitStack();
    }

    private void InitStack()
    {
        Dictionary<int, int> valueMap = new();
        for (int i = 0; i <= max; i++)
        {
            valueMap.Add(i, 0);
        }
        
        for (int i = 0; i < size; i++)
        {
            int value;
            if (i > size / (max + 1))
            {
                var map = CountExistingValues();
                var list = FindMinKeyValue(map);

                value = list[Random.Range(0, list.Count)];
            }
            else
            {
                value = Random.Range(min, max + 1);
            }

            //Debug.Log($"index: {i} value: {value}");

            valueMap[value]++;
            values.Push(value);
        }

        for (int i = 0; i < valueMap.Count; i++)
        {
            Debug.Log($"index: {i} count {valueMap[i]}");
        }
    }
    
    public int GetRandomInt()
    {
        if (values.Count == 0)
        {
            InitStack();
        }
        return values.Pop();
    }

    private List<int> FindMinKeyValue(Dictionary<int, int> map)
    {
        var minValue = map[0];
        
        for (int i = 0; i < map.Count; i++)
        {
            if (map[i] < minValue)
            {
                minValue = map[i];
            }
        }

        List<int> result = new();
        
        for (int i = 0; i < map.Count; i++)
        {
            if (map[i] == minValue)
            {
                result.Add(i);
            }
        }

        return result;
    }
    
    private Dictionary<int, int> CountExistingValues()
    {
        Dictionary<int, int> valueMap = new();

        for (int i = min; i <= max; i++)
        {
            valueMap.Add(i, 0);
        }
        
        foreach (var number in values)
        {
            valueMap[number]++;
        }

        return valueMap;
        
        int sum = 0;
        
        for (int i = 0; i < valueMap.Count; i++)
        {
            sum += valueMap[i];
        }

        for (int i = 0; i < valueMap.Count; i++)
        {
            valueMap[i] = sum - valueMap[i];
            //Debug.Log($"key: {i} value: {valueMap[i]}");
        }
        
        var value = Random.Range(0, sum + 1);
        var currentWeight = 0;

        //Debug.Log($"value: {value} sum: {sum}");
        for (int i = 0; i < valueMap.Count; i++)
        {
            currentWeight += valueMap[i];
            
            if (value <= currentWeight)
            {
                //return i;
            }
        }

       //return 0;
    }
}