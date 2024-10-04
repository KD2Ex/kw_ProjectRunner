using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class PriorityChunk
{
    public readonly Chunk Chunk;
    public readonly int Priority;
    
    public PriorityChunk(Chunk chunk, int priority)
    {
        Chunk = chunk;
        Priority = priority;
    }
}

public class ChunkRandomManager : MonoBehaviour
{
    [SerializeField] private List<ChunkSet> chunkSets;

    public List<PriorityChunk> SpawnQueue;

    public void Execute()
    {
        AddChunkToQueue();
    }
    
    private static int CompareByPriority(PriorityChunk x, PriorityChunk y)
    {
        return x.Priority - y.Priority;
    }
    
    private void AddChunkToQueue()
    {
        var chunks = GetNextChunks();
        chunks.Sort(CompareByPriority);
        
        foreach (var chunk in chunks)
        {
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
            var conditionSatisfied = set.SpawnCondition.Evaluate();

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
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var list in chunkSets)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
