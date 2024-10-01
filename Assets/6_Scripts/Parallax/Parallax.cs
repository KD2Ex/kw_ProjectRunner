using UnityEngine;

public class Parallax : MonoBehaviour
{
    #region Serialize Fileds
    
    [SerializeField] private GameObject background;
    [SerializeField] private bool instantiateFirstBackground;
    
    #endregion

    #region Compoonents
    
    private Transform player;
    private SpriteRenderer sprite;
    private GameObject nextBackground;
    
    #endregion

    #region Constants
    
    private const float screenSize = 18f; // replace with SO data

    #endregion
    
    #region Members
    
    private bool m_IsBackgroundLoaded;
    private bool m_IsBackgroundCleared;
    private Bounds m_Bounds;
    
    #endregion
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (instantiateFirstBackground)
        {
            background = Instantiate(background, transform);
        }
        
        sprite = background.GetComponent<SpriteRenderer>();
        SetNewBounds(sprite.bounds);
    }

    void Update()
    {
        var distanceToPlayer = m_Bounds.extents.x + background.transform.position.x - player.position.x;

        AddBackgroundDependingOn(distanceToPlayer);
        RemoveBackgroundDependingOn(distanceToPlayer);
    }

    private void AddBackgroundDependingOn(float distanceToPlayer)
    {
        if (distanceToPlayer < screenSize + 2f && !m_IsBackgroundLoaded)
        {
            DuplicateBackground(background);
            m_IsBackgroundLoaded = true;
            m_IsBackgroundCleared = false;
        }
    }

    private void RemoveBackgroundDependingOn(float distanceToPlayer)
    {
        if (distanceToPlayer < -6f && !m_IsBackgroundCleared)
        {
            ClearBackground(background);
            m_IsBackgroundCleared = true;
            m_IsBackgroundLoaded = false;
        }
    }
    
    private void DuplicateBackground(GameObject back)
    {
        var next = Instantiate(back, transform);
        next.transform.position += new Vector3(m_Bounds.extents.x * 2, 0, 0);
        nextBackground = next;
    }

    private void ClearBackground(GameObject back)
    {
        back.SetActive(false);
        background = nextBackground;
        nextBackground = null;
    }
    
    private void SetNewBounds(Bounds bounds)
    {
        m_Bounds = bounds;
    }
}
