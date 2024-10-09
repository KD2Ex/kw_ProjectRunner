using UnityEngine.Events;

public class Shield : Booster
{
    public UnityEvent OnShieldDisable;
    
    protected override void OnEnable()
    {
        //Debug.Log(gameObject.name);
    }
    
    protected override void OnDisable()
    {
        OnShieldDisable?.Invoke();
        //Debug.Log(gameObject.name);
    }
}
