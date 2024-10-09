using System;
using UnityEngine;

public class FoodUse : MonoBehaviour
{
    [SerializeField] private InputReader input;
    [SerializeField] private Animator animator;
    private void OnEnable()
    {
        input.DisableGameplayInput();
    }

    private void OnDisable()
    {
        input.EnableGameplayInput();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartAnimation()
    {
        input.DisableGameplayInput();
    }

    public void StopAnimation()
    {
        input.EnableGameplayInput();
        Destroy(this);
    }
}
