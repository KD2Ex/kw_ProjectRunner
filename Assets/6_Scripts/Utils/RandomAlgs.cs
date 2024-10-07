using System.Collections.Generic;
using UnityEngine;

public class ChunkRandomizer
{
    private static ChunkRandomizer m_Instance;

    public static ChunkRandomizer GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new ChunkRandomizer();
        }

        return m_Instance;
    }

    public Chunk GetRandomChunk(List<Chunk> items)
    {
        var index = Random.Range(0, items.Count);

        return items[index];
    }
}