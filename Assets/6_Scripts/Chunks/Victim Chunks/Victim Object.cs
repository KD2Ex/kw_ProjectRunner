using UnityEngine;

public class VictimObject : Enemy
{
    [SerializeField] private InputReader input;

    [SerializeField] private GameObject vod;
    
    [SerializeField] private ApproachingButton stopButton;

    /*
    private void OnEnable()
    {
        input.InteractEvent += Interact;
    }

    private void OnDisable()
    {
        input.InteractEvent -= Interact;
    }
    */

    private void Update()
    {
        if (DistanceToPlayer < 14f)
        {
            stopButton.FadeIn();
        }
    }

    private void Interact()
    {
        if (vod.activeInHierarchy) return;
        
        vod.SetActive(true);
    }
}
