using System;
using UnityEngine;

public class SyncAnimation : MonoBehaviour
{
    [SerializeField] private string m_AnimationName;
    
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Sync(float offset)
    {
        offset = Convert.ToSingle(offset - Math.Truncate(offset));
        
        animator.Play(m_AnimationName, 0, offset);
        Debug.Log(offset);
    }
}
