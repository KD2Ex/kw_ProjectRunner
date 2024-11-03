using UnityEngine;

[RequireComponent(typeof(ConstantMovement))]
public class Deer : Enemy
{
    [SerializeField] private ConstantMovement movement;
    
    private void Start()
    {
        movement.enabled = false;
        distance = 20f; // screen size + 2f
    }

    private void Update()
    {
        if (movement.enabled) return;
        
        if (PlayerInDistance) 
        {
            transform.SetParent(null, true);
            movement.enabled = true;
        }
    }
}
