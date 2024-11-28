using UnityEngine;

public class DashIndicator : MonoBehaviour
{
    [SerializeField] private FloatVariable maxEnergy;
    [SerializeField] private FloatVariable currentDashEnergy;
    private Animator animator;

    [SerializeField] private AudioSource audioSource;
    /*
    private int Zero = Animator.StringToHash("dash_indicator_zero");
    private int One= Animator.StringToHash("dash_indicator_one");
    private int Three = Animator.StringToHash("dash_indicator_three");
    private int Four = Animator.StringToHash("dash_indicator_four");
    private int Five = Animator.StringToHash("dash_indicator_five");
    */
    
    private int Zero = Animator.StringToHash("Canvas_DashIndicator_1");
    private int One = Animator.StringToHash("Canvas_DashIndicator_2");
    private int Three = Animator.StringToHash("Canvas_DashIndicator_3");
    private int Four = Animator.StringToHash("Canvas_DashIndicator_4");
    private int Five = Animator.StringToHash("Canvas_DashIndicator_5");

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
        if (stage == 5)
        {
            audioSource.Play();
        }
        animator.Play(hashes[stage]);
    }
}
