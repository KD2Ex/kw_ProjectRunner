using UnityEngine;

public class Shield : Booster
{
    [SerializeField] private InvincibilityController invincibilityController;    
    
    protected override void OnEnable()
    {
        //Debug.Log(gameObject.name);
    }
    
    protected override void OnDisable()
    {
        invincibilityController.Trigger();
        //Debug.Log(gameObject.name);
    }
}
