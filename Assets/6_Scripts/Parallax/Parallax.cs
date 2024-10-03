using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    #region Serialize Fileds
    
    [SerializeField] private List<GameObject> m_Backgrounds;
    [SerializeField] private bool instantiateFirstBackground;
    
    #endregion

    #region Compoonents
    
    private Transform player;
    private SpriteRenderer sprite;
    private GameObject currentBackground;
    private GameObject nextBackground;
    
    #endregion

    #region Constants
    
    private const float screenSize = 18f; // replace with SO data

    #endregion
    
    #region Members
    
    private bool m_IsBackgroundLoaded;
    private bool m_IsBackgroundCleared;
    private Bounds m_Bounds;
    private int index = 0;
    private Dictionary<string, Bounds> m_SpritesBounds = new();
    private float m_xOffset = 0f;
    
    #endregion

    private void Awake()
    {
        if (instantiateFirstBackground)
        {
            currentBackground = Instantiate(m_Backgrounds[0], transform);
            index++;
        }

        foreach (var go in m_Backgrounds)
        {
            var spriteRenderer = go.GetComponent<SpriteRenderer>();
            m_SpritesBounds.Add(go.name, spriteRenderer.bounds);

            Debug.Log(go.name);
        }
        
        sprite = currentBackground.GetComponent<SpriteRenderer>();
        SetNewBounds(sprite.bounds);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        var distanceToPlayer = m_Bounds.extents.x + currentBackground.transform.position.x - player.position.x;

        //Debug.Log("currentBackgrouned: " + currentBackground.transform.position.x);
        //Debug.Log("distance: " + distanceToPlayer);
        //Debug.Log("BoundsX: " + m_Bounds);
        
        AddBackgroundDependingOn(distanceToPlayer);
    }

    private void AddBackgroundDependingOn(float distanceToPlayer)
    {
        if (distanceToPlayer < screenSize + 2f )
        {
            if (index == m_Backgrounds.Count) index = 0;
            DuplicateBackground(m_Backgrounds[index]);
            index++;
        }
    }

    private void RemoveBackgroundDependingOn(float distanceToPlayer)
    {
        if (distanceToPlayer < -6f && !m_IsBackgroundCleared)
        {
            ClearBackground(currentBackground);
            m_SpritesBounds.TryGetValue(currentBackground.name, out var newBounds);
            Debug.Log(currentBackground.name);
            Debug.Log(newBounds);
            SetNewBounds(newBounds);
            
            m_IsBackgroundCleared = true;
            m_IsBackgroundLoaded = false;
        }
    }
    
    private void DuplicateBackground(GameObject back)
    {
        var next = Instantiate(back, transform);
        next.name = back.name;
        
        back.TryGetComponent<Animator>(out var animator);

        if (animator)
        {
            Debug.Log("animator gotten");
            
            var time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            next.GetComponent<SyncAnimation>().Sync(time);
        }

        m_SpritesBounds.TryGetValue(next.name, out var newBounds);

        var offset = m_Bounds.extents.x + newBounds.extents.x;
        Debug.Log(offset);
        Debug.Log(m_Bounds.extents.x);
        Debug.Log(newBounds.extents.x);
        m_xOffset += offset;
        
        SetNewBounds(newBounds);

        next.transform.position += new Vector3(m_xOffset, 0, 0);
        currentBackground = next;
    }

    private void ClearBackground(GameObject back)
    {
        back.SetActive(false);
        currentBackground = nextBackground;
        nextBackground = null;
    }
    
    private void SetNewBounds(Bounds bounds)
    {
        m_Bounds = bounds;
    }
}
