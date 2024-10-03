using System;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    public static PlayerLocator instance;
    public Transform playerTransform { get; private set; }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
}