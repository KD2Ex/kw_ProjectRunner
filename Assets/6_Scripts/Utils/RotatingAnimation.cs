using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum Direction
{
    LEFT = 1,
    RIGHT = -1
}

public class RotatingAnimation : MonoBehaviour
{
    [SerializeField] private float maxDeviation;
    [SerializeField] private float rate;
    [SerializeField] private Direction startDirection; 
        
    private float elapsed = 0f;
    private int direction;

    private void Awake()
    {
        direction = (int) startDirection;
    }

    private void Update()
    {
        elapsed += Time.deltaTime * rate * direction;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, elapsed));

        if (Mathf.Abs(elapsed) >= maxDeviation)
        {
            direction *= -1;
        }
        return;
        var targetRot = Quaternion.AngleAxis(Time.deltaTime, Vector3.forward);
        transform.rotation = 
            Quaternion.RotateTowards(transform.rotation, targetRot, 6f * Time.deltaTime);
    }
}
