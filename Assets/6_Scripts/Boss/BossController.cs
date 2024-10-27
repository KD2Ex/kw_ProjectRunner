using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossAttack[] attacks;
    
    public bool Attacking;

    private PooledRandom pooledRandom;
    
    private PlayerBossController Player => GameManager.instance.PlayerBossController;

    private void Awake()
    {
        pooledRandom = new PooledRandom(100, 0, 2);
    }

    void Update()
    {
        //Debug.Log($"Boss attacking: {Attacking}");
        
        if (Player.Dead) enabled = false;

        if (Attacking) return;
        
        Attack();
    }
    
    private void Attack()
    {
        Attacking = true;

        var index = pooledRandom.GetRandomInt();
        attacks[index].gameObject.SetActive(true);
    }
    
    public bool IsAnyAttackActive()
    {
        foreach (var attack in attacks)
        {
            if (attack.gameObject.activeInHierarchy) return true;
        }

        return false;
    }

    public void AttackEnded()
    {
        if (IsAnyAttackActive()) return;
        
        StartCoroutine(Coroutines.WaitFor(1f, null, () => Attacking = false));
    }
}
