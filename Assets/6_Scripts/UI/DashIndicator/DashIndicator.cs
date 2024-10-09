using UnityEngine;
using UnityEngine.Serialization;

public class DashIndicator : MonoBehaviour
{
    [SerializeField] private FloatVariable maxEnergy;
    [SerializeField] private FloatVariable currentDashEnergy;
    private Animator animator;

    private int Zero = Animator.StringToHash("dash_indicator_zero");
    private int One= Animator.StringToHash("dash_indicator_one");
    private int Three = Animator.StringToHash("dash_indicator_three");
    private int Four = Animator.StringToHash("dash_indicator_four");
    private int Five = Animator.StringToHash("dash_indicator_five");

    private int[] hashes;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        hashes = new[] {Zero, One, Three, Four, Five};
    }

    void Update()
    {
        int stage = (int) currentDashEnergy.Value / ((int)maxEnergy.Value / 5);
        PlayAnimationAtStage(stage);
    }

    private void PlayAnimationAtStage(int stage)
    {
        if (stage >= hashes.Length) return;
        
        animator.Play(hashes[stage]);
    }
}
