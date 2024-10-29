using UnityEngine;

public class CustomSizeChunk : MonoBehaviour
{
    [SerializeField] private Transform LeftExtent;
    [SerializeField] private Transform RightExtent;

    public float Size
    {
        get
        {
            var res = RightExtent.localPosition.x - LeftExtent.localPosition.x;
            Debug.Log(res);
            return res;
        }
    }

    public float LeftExtentLocalX => LeftExtent.localPosition.x;
    public float RightExtentLocalX => RightExtent.localPosition.x;
    public float RightExtentX => RightExtent.position.x;
}