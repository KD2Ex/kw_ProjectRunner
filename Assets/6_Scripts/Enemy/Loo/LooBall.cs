public class LooBall : Enemy
{
    private bool hasTriggered;

    void Update()
    {
        if (PlayerInDistance && !hasTriggered)
        {
            animator.Play("LooBallMovement");
            hasTriggered = true;
        }    
    }
}
