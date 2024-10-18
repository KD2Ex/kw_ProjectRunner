using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class Agaric : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private GameObject attack;

    [SerializeField] private AudioSource idleSound;
    [SerializeField] private AudioSource attackSound;

    private Animator animator;
    
    private bool attacked;
    private Transform player => PlayerLocator.instance.playerTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        idleSound.enabled = true;
    }

    void Update()
    {
        var currDist = (player.position - transform.position).magnitude;

        if (currDist < idleSound.maxDistance && !idleSound.isPlaying)
        {
            if (idleSound.enabled)
            {
                idleSound.Play();
            }
        }
        
        if (currDist > distance) return;
        if (attacked) return;
        
        attacked = true;

        attackSound.enabled = true;
        idleSound.enabled = false;
        attackSound.Play();
        
        
        animator.Play("Agaric_attack");

        StartCoroutine(PlaySound());
        
        var instance = Instantiate(attack, transform);
        instance.SetActive(true);
    }

    private IEnumerator PlaySound()
    {
        Debug.Log(attackSound.time);
        yield return null;
    }
}
