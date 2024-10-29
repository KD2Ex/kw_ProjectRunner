using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BecomeParallax : MonoBehaviour
{
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.parent.position;
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
