using UnityEngine;

public class DigitObject : MonoBehaviour
{
    private Animator m_Animator;
    [SerializeField] private AnimationClip clip;

    private float elapsedTime = 0;
    private float index = 0;

    public int Digit;

    public void SetDigit(int digit)
    {
        Digit = digit;
        float time = digit / 10f;
        m_Animator.Play(clip.name, 0, time);
    }
    
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.speed = 0;
    }
}
