using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PriorityChunk
{
    public  Chunk Chunk;
    public  int Priority;
    
    public PriorityChunk(Chunk chunk, int priority)
    {
        Chunk = chunk;
        Priority = priority;
    }
}

[CreateAssetMenu(fileName = "Chunk Random Manager", menuName = "Scriptable Objects/Chunks/Random Manager")]
public class ChunkRandomManager : ScriptableObject
{
    [SerializeField] private List<ChunkSet> chunkSets;
    [SerializeField] private List<PriorityChunk> SpawnQueue;

    public List<ChunkSet> Sets => chunkSets;
    public List<GameObject> PresentChunks;
    
    private void OnEnable()
    {
        
    }

    public void InitializeChunks()
    {
        foreach (var set in chunkSets)
        {
            set.InitCondition();
            
            foreach (var chunk in set.List.Items)
            {
                chunk.Initialize();
            }
        }
    }
    
    public PriorityChunk Pop()
    {
        if (SpawnQueue.Count == 0)
        {
            AddChunkToQueue();
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
            foreach (var chunk in chunkSet.List.Items)
            {
                if (chunk.Available) continue;
            }
        }
    }
    
    public void AddChunkToQueue()
    {
        var chunks = GetNextChunks();
        chunks.Sort(CompareByPriority);
        
        foreach (var chunk in chunks)
        {
            if (chunk.Chunk.RemoveAfterSpawn) chunk.Chunk.Available = false;
            SpawnQueue.Add(chunk);
        }
    }
    
    private List<PriorityChunk> GetNextChunks()
    {
        var spawnReady = GetReadyToSpawnSets(chunkSets);
        var highestPrioritySets = GetHighestPrioritySets(spawnReady);

        var result = new List<PriorityChunk>();
        foreach (var set in highestPrioritySets)
        {
            set.Condition.ResetTrigger();
            var nextChunk = GetChunkFromList(set.List.Items, FindChunkByWeight);
            
            result.Add(new PriorityChunk(nextChunk, set.Priority));
        }

        return result;
    }

    private List<ChunkSet> GetReadyToSpawnSets(List<ChunkSet> sets)
    {
        var result = new List<ChunkSet>();
        
        foreach (var set in sets)
        {
            var conditionSatisfied = set.Condition.Evaluate();
            if (conditionSatisfied)
            {
                var availableChunkPresent = set.List.Items.FirstOrDefault(chunk => chunk.Available);
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

    private Chunk GetChunkFromList(List<Chunk> list, Func<List<Chunk>, Chunk> randomAlg)
    {
        var readyChunks = list.Where(chunk => chunk.Available).ToList();
        
        return randomAlg(readyChunks);
    }

    private Chunk FindChunkByWeight(List<Chunk> list)
    {
        var index = Random.Range(0, list.Count);

        return list[index];
    }
}
