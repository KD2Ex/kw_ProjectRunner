using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class DigitObject : MonoBehaviour
{
    private Animator m_Animator;
    [SerializeField] private AnimationClip clip;

    private float elapsedTime = 0;
    private float index = 0;

    public void Initialize(int digit)
    {
        float time = digit / 10f;
        Debug.Log(time);
        
        m_Animator.Play(clip.name, 0, time);
    }
    
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Animator.speed = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    /*void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > .4f)
        {
            elapsedTime = 0f;
            m_Animator.Play(clip.name, 0, index);

            index += .1f;
            Debug.Log("COUNT");
        }
    }*/
}
