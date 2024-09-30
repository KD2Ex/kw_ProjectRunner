using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongsideX : MonoBehaviour
{
    private Vector3 Pos => transform.position;
    
    public void By(float x)
    {
        transform.position = new Vector3(Pos.x + x, Pos.y, 0f);
    }
}
