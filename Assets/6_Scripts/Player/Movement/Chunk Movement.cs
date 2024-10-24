using UnityEngine;

public class ChunkMovement : MonoBehaviour
{
    [SerializeField] private ChunkRuntimeSet chunks;
    
    public void Save(ref ChunkPositionData data)
    {
        data.xPosition = transform.position.x - chunks.Items[0].x;
        Debug.Log(data.xPosition);
    }

    public void Load(ChunkPositionData data)
    {
        transform.position = new Vector3(data.xPosition, 0f, 0f);
    }
}

[System.Serializable]
public struct ChunkPositionData
{
    public float xPosition;
}
