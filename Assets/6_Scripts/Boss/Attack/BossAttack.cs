using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private BossController boss;
    private Animator animator;

    private void Awake()
    {
        boss = GetComponentInParent<BossController>();
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        boss.AttackEnded();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.instance.PlayerBossController.CheckHealth();
    }

}
