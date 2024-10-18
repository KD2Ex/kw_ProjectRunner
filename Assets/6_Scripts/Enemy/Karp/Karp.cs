using System.Collections;
using UnityEngine;

public class Karp : Enemy
{
    [SerializeField] private Transform raySpawnPoint;
    
    [SerializeField] private KarpRay ray;

    [SerializeField] private AudioSource idle;
    [SerializeField] private AudioSource attack;
    
    private bool engage;

    public Transform RayPoint => raySpawnPoint;
    
    private void Start()
    {
        Attack();
    }

    private IEnumerator SpawnRay()
    {
        Debug.Log("spawn ray");
        
        animator.Play("karp_attack");
        idle.Pause();
        attack.Play();
        var elapsed = 0f;

        while (elapsed < animator.GetCurrentAnimatorStateInfo(0).length)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        ray.gameObject.SetActive(true);
        
        attack.Stop();
        idle.UnPause();
        animator.Play("karp_idle");
    }

    private void Update()
    {
        if (engage) return;

        Attack();
    }

    private void Attack()
    {
        StartCoroutine(SpawnRay());
        engage = true;
    }

    public void ResetAttack()
    {
        engage = false;
    }

    private IEnumerator PlayAttackSound()
    {
        var elapsed = 0f;
        idle.Pause();
        attack.Play();

        while (elapsed < attack.clip.length)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        idle.UnPause();
    }
}
