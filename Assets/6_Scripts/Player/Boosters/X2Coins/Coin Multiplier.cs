using UnityEngine;

public class CoinMultiplier : Booster
{
    [SerializeField] private FloatVariable Multiplier;
    public float Value => gameObject.activeSelf ? Multiplier.Value : 1f;
}
