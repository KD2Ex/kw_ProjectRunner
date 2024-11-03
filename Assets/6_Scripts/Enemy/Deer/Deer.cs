using UnityEngine;

[RequireComponent(typeof(ConstantMovement))]
public class Deer : Enemy
{
    [SerializeField] private ConstantMovement movement;
    [SerializeField] private ClampedValue<float> animSpeed;
    private readonly int hash = Animator.StringToHash("AnimSpeed");
    
    private void Start()
    {
        movement.enabled = false;
        distance = 20f; // screen size + 2f

        var value = Random.Range(animSpeed.min, animSpeed.max);
        animator.SetFloat(hash, value);
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
