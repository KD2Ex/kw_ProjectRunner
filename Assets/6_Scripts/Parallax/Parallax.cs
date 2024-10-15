using System;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Serializable]
    public struct Background
    {
        public GameObject Prefab;
        public SpriteRenderer Sprite;
    }
    
    #region Serialize Fileds
    
    [SerializeField] private List<GameObject> m_Backgrounds;
    [SerializeField] private List<Background> Backgrounds;
    [SerializeField] private Transform target;
    
    [SerializeField] private bool instantiateFirstBackground;

    #endregion

    #region Compoonents
    
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
            var compute = go.GetComponent<ComputeBounds>();
            var bounds = compute.GetBounds();
            
            //var spriteRenderer = go.GetComponent<SpriteRenderer>();
            m_SpritesBounds.Add(go.name, bounds);
        }
        
        //sprite = currentBackground.GetComponent<SpriteRenderer>();
        SetNewBounds(m_SpritesBounds[m_Backgrounds[0].name]);
    }

    void Update()
    {
        var distanceToPlayer = m_Bounds.extents.x + currentBackground.transform.position.x - target.transform.position.x;

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

    private void DuplicateBackground(GameObject back)
    {
        var next = Instantiate(back, transform);
        next.name = back.name;
        
        currentBackground.TryGetComponent<Animator>(out var animator);
        next.TryGetComponent<GetDestroyedIfFarBehindPlayer>(out var getDestroyed);
        if (getDestroyed) getDestroyed.SetTarget(target);
        
        if (animator)
        {
            var time = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            next.GetComponent<SyncAnimation>().Sync(time);
        }

        m_SpritesBounds.TryGetValue(next.name, out var newBounds);

        var offset = m_Bounds.extents.x + newBounds.extents.x;

        m_xOffset += offset;
        
        SetNewBounds(newBounds);

        next.transform.position += new Vector3(m_xOffset, 0, 0);
        currentBackground = next;
    }
    
    private void SetNewBounds(Bounds bounds)
    {
        m_Bounds = bounds;
    }
}
