using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PriorityChunk
{
    //public  Chunk Chunk;
    public  IChunk ChunkSO;
    public  int Priority;
    
    public PriorityChunk(/*Chunk chunk, */IChunk chunksSo, int priority)
    {
        //Chunk = chunk;
        ChunkSO = chunksSo;
        Priority = priority;
    }
}

[CreateAssetMenu(fileName = "Chunk Random Manager", menuName = "Scriptable Objects/Chunks/Random Manager")]
public class ChunkRandomManager : ScriptableObject
{
    [SerializeField] private List<ChunkSet> chunkSets;
    [SerializeField] private List<IChunk> SpawnQueue = new(); // replace with List<GameObjects>

    public List<ChunkSet> Sets => chunkSets;
    public bool IsQueueEmpty => SpawnQueue.Count == 0;
    
    private void OnEnable()
    {
        
    }

    public void ClearQueue()
    {
        SpawnQueue.Clear();
    }

    public void InitializeChunks()
    {
        foreach (var set in chunkSets)
        {
            set.InitCondition();
            
            foreach (var chunk in set.List.TestItems) // set.List.Items
            {
                if (set.List.RestoreOnEnable) chunk.Restore();
                //Debug.Log(chunk);
                chunk.Initialize();
            }
        }
    }
    
    public IChunk Pop()
    {
        if (SpawnQueue.Count == 0)
        {
            AddRandomChunkToQueue();
        }
        
        var result = SpawnQueue[0];
        
        SpawnQueue.RemoveAt(0);
        return result;
    }

    private static int CompareByPriority(PriorityChunk x, PriorityChunk y)
    {
        return x.Priority - y.Priority;
    }

    public void RestoreAvailability()
    {
        foreach (var chunkSet in chunkSets)
        {
            foreach (var chunk in chunkSet.List.TestItems) // chunkSet.List.Items
            {
                if (chunk.Available) continue;
                if (chunk.Condition == null) continue;
                var evalResult = chunk.Condition.Evaluate();
                if (evalResult)
                {
                    chunk.Condition.ResetTrigger();
                    chunk.Restore();
                }
            }
        }
    }

    public void AddChunkToQueue(GameObject prefab, int priority = 1)
    {
        Chunk chunk = new Chunk(prefab);
        //var pch = new PriorityChunk(chunk, 1);
        SpawnQueue.Add(chunk);
    }
    
    public void AddRandomChunkToQueue()
    {
        var chunks = GetNextChunks();
        chunks.Sort(CompareByPriority);

        //Debug.Log(chunks.Count);
        
        foreach (var chunk in chunks)
        {
            if (chunk.ChunkSO.BecomeUnavailableAfterSpawn) chunk.ChunkSO.Available = false;
            SpawnQueue.Add(chunk.ChunkSO); // chunks.Chunk.Prefab
        }
    }
    
    private List<PriorityChunk> GetNextChunks()
    {
        var spawnReady = GetReadyToSpawnSets(chunkSets);
        var highestPrioritySets = GetHighestPrioritySets(spawnReady);

        var result = new List<PriorityChunk>();

        var resChunk = GetChunkFromSOData(
            highestPrioritySets[Random.Range(0, highestPrioritySets.Count)].List.TestItems,
            FindChunkByWeight);
        
        result.Add(new PriorityChunk(resChunk, highestPrioritySets[0].Priority));
        return result;
        
        foreach (var set in highestPrioritySets)
        {
            //set.Conditions.ResetTrigger();
            set.ResetAll();
            var nextChunk = GetChunkFromSOData(set.List.TestItems, FindChunkByWeight); // chunkSet.List.Items
            
            result.Add(new PriorityChunk(nextChunk, set.Priority));
        }

        return result;
    }

    private List<ChunkSet> GetReadyToSpawnSets(List<ChunkSet> sets)
    {
        var result = new List<ChunkSet>();
        
        foreach (var set in sets)
        {
            var conditionSatisfied = set.EvaluateAll();
            //var conditionSatisfied = set.Conditions.Evaluate();
            if (conditionSatisfied)
            {
                var availableChunkPresent = set.List.TestItems.FirstOrDefault(chunk => chunk.Available);
                if (availableChunkPresent != default) result.Add(set);
            }
        }

        return result;
    }

    private List<ChunkSet> GetHighestPrioritySets(List<ChunkSet> sets)
    {
        var result = new List<ChunkSet>();
        var highestPriority = 0;
        
        foreach (var set in sets)
        {
            if (set.Priority > highestPriority)
            {
                highestPriority = set.Priority;
            }
        }

        result = sets.FindAll(set => set.Priority >= highestPriority);

        return result;
    }

    private IChunk GetChunkFromList(List<IChunk> list, Func<List<IChunk>, IChunk> randomAlg)
    {
        var readyChunks = list.Where(chunk => chunk.Available).ToList();
        
        return randomAlg(readyChunks);
    }
    
    private IChunk GetChunkFromSOData(List<ChunkSOData> list, Func<List<ChunkSOData>, IChunk> randomAlg)
    {
        var readyChunks = list.Where(chunk => chunk.Available).ToList();
        
        return randomAlg(readyChunks);
    }

    private IChunk FindChunkByWeight(List<ChunkSOData> list)
    {
        IChunk result = list[0];
        
        int sum = 0;
        list.ForEach(chunk => sum += chunk.Weight);

        var randomWeight = Random.Range(0, sum);

        var weightCount = 0;
        foreach (var chunk in list)
        {
            weightCount += chunk.Weight;
            if (randomWeight <= weightCount)
            {
                result = chunk;
                break;
            }
        }

        return result;
        
        var index = Random.Range(0, list.Count);
        return list[index];
    }
}
