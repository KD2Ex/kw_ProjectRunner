using UnityEngine;

public class Shield : Booster
{
    [SerializeField] private InvincibilityController invincibilityController;    
    
    protected override void OnEnable()
    {
        base.OnEnable();
        //Debug.Log(gameObject.name);
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        invincibilityController.Trigger();
        //Debug.Log(gameObject.name);
    }
}
