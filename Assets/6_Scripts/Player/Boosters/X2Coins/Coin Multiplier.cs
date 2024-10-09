using UnityEngine;

public class CoinMultiplier : Booster
{
    [SerializeField] private FloatVariable Multiplier;
    public float Value => gameObject.activeSelf ? Multiplier.Value : 1f;

    protected override void OnEnable()
    {
        Debug.Log(gameObject.name);
    }
    
    protected override void OnDisable()
    {
        Debug.Log(gameObject.name);
    }
}
