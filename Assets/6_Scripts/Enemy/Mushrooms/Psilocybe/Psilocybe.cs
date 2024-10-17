using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Psilocybe : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    void Start()
    {
        animator.SetFloat("AnimSpeed", 1 + Random.Range(0f, 1.5f));      
    }

    private void Update()
    {
        var dist = (PlayerLocator.instance.playerTransform.position - transform.position).magnitude;
        if (dist < 24f)
        {
            animator.Play("psilocybe");
        }
    }
}
