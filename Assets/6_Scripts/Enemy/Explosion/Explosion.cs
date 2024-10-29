using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float distanceToExplode;
    [SerializeField] private AudioClip clip;
    
    private Animator animator;
    private bool exploded;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (exploded) return;

        if (GameManager.instance.Player.CurrentState.ToString() == "DeathState"
            && PlayerLocator.instance.DistanceToPlayer(transform) <= distanceToExplode * 2)
        {
            Explode();
            return;
        }
        
        if (PlayerLocator.instance.DistanceToPlayer(transform) <= distanceToExplode)
        {
            Explode();
        }
    }

    private void Explode()
    {
        animator.Play("explosion");
        SoundFXManager.instance.PlayClipAtPoint(clip, transform, .7f);
        exploded = true;
    }
}
