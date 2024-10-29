using UnityEngine;

public interface IInvincible
{
    SpriteRenderer Sprite { get; set; }
    public bool IsInvincible();
}