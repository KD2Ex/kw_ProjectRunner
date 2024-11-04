using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    [SerializeField] private BossAttack[] attacks;

    private bool CanAttack;
    
    public bool Attacking;

    private PooledRandom pooledRandom;
    
    private PlayerBossController Player => GameManager.instance.PlayerBossController;

    private void Awake()
    {
        pooledRandom = new PooledRandom(100, 0, 2);
    }

    private void Start()
    {
        //StartCoroutine(Coroutines.WaitFor(clip.length, null, Finish));

        StartCoroutine(WaitUntil());
    }

    private IEnumerator WaitUntil()
    {
        CanAttack = false;
        yield return new WaitUntil(() => SceneManager.loadedSceneCount == 2);
        Debug.Log("All Scenes Loaded");
        yield return new WaitForSeconds(2f);
        CanAttack = true;
    }

    private void Finish()
    {
        // open score panel

        SceneManager.LoadScene("Main");
    }

    void Update()
    {
        //Debug.Log($"Boss attacking: {Attacking}");
        if (!CanAttack) return;
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
