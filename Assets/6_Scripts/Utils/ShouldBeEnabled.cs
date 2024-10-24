using System;
using UnityEngine;

public class ShouldBeEnabled : MonoBehaviour
{
    [SerializeField] private bool flag;

    private void Awake()
    {
        if (flag)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Debug.Log(gameObject.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
