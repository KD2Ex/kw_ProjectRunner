using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Collider2D m_Collider;
    private Animator m_Animator;
    
    private void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
        m_Animator = GetComponent<Animator>();
    }
    
    public void GetDestroyed()
    {
        m_Collider.enabled = false;
        m_Animator.SetTrigger("Destroy");
        // play sound
    }
}
