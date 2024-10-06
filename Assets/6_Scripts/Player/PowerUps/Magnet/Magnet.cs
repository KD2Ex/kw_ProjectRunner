using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Magnet : PowerUp
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Coin")) return;
        
        
    }
}