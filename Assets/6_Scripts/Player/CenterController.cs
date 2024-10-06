using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Align
{
    Top,
    Center
}

[Serializable]
public class Alignment
{
    public Align Align;
    public Transform Transform;
    
    public Alignment(Align align, Transform transform)
    {
        Align = align;
        Transform = transform;
    }
}

public class CenterController : MonoBehaviour
{

    [SerializeField] private List<GameObject> m_ObjectsToAlign;
    [SerializeField] private List<Alignment> m_Alignments;
    [SerializeField] private float changeSpeed;
    private Align CurrentAlignment;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeAlignment(Align newAlign)
    {
        CurrentAlignment = newAlign;
        var target = m_Alignments
            .Find(alignment => alignment.Align == CurrentAlignment);

        foreach (var o in m_ObjectsToAlign)
        {
            StartCoroutine(TranslateToAlignmentPoint(target.Transform, o.transform));
        }
    }

    private IEnumerator TranslateToAlignmentPoint(Transform target, Transform objectToAlign)
    {
        var distance = (target.position - objectToAlign.position).magnitude;
        
        while (distance > .1f)
        {
            var dir = (target.position - objectToAlign.position).normalized;
            objectToAlign.transform.Translate(dir * (changeSpeed * Time.deltaTime));
            
            /*
            objectToAlign.transform.position = 
                Vector3
                    .MoveTowards(objectToAlign.transform.position, target.position, .1f);
                    */

            distance = (target.position - objectToAlign.position).magnitude;
            yield return null;
        }
    }
    
    
}
