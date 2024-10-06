using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class TransformPoint
{
    public Align Align;
    public Transform Transform;
    
    public TransformPoint(Align align, Transform transform)
    {
        Align = align;
        Transform = transform;
    }
}


public class PlayerBoostersParentController : MonoBehaviour
{
    [SerializeField] private List<TransformPoint> m_Parents;

    public void ChangeParent(Align align)
    {
        transform.SetParent(m_Parents.First(parent => parent.Align == align).Transform);
        transform.localPosition = Vector3.zero;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
