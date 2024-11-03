using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAppearance : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private string bossSceneName;

    [SerializeField] private GameObject UIButton;
    
    protected float DistanceToPlayer => PlayerLocator.instance.DistanceToPlayer(transform);
    
    private void Accept()
    {
        SceneManager.LoadScene("Boss Persistence");
        var async = SceneManager.LoadSceneAsync(bossSceneName, LoadSceneMode.Additive);
        
        GameManager.instance.OpenLoadingScreen(async);
    }
    
    private bool start;
    
    void Update()
    {
        if (start) return;
        
        if (DistanceToPlayer < .1f)
        {
            Appear();
            start = true;
        }
    }

    public virtual void Appear()
    {
        input.InteractEvent += Accept;
        UIButton.SetActive(true);
    }

    public virtual void Disappear()
    {
        input.InteractEvent -= Accept;
        Destroy(UIButton.gameObject);
    }
}