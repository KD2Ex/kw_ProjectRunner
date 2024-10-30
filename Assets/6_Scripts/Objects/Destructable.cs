using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private Collider2D[] m_Colliders;
    private Animator m_Animator;
    
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
    
    public void GetDestroyed()
    {
        foreach (var mCollider in m_Colliders)
        {
            mCollider.enabled = false;
        }
        m_Animator.SetTrigger("Destroy");
        source.Play();
        // play sound
    }
}
