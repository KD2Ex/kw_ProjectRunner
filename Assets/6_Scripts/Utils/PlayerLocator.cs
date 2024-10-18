using System;
using UnityEngine;

public class PlayerLocator : MonoBehaviour
{
    public static PlayerLocator instance;
    public Transform playerTransform { get; private set; }
    public Player player { get; private set; }
    public Vector3 DefaultPosition;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        DefaultPosition = playerTransform.position;
    }

    public float DistanceToPlayer(Transform origin)
    {
        return origin.position.x - playerTransform.position.x;
    }

}