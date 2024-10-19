using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    protected AudioClip clip;

    public virtual void Pickup(Player player)
    {
        gameObject.SetActive(false);
        SoundFXManager.instance.PlayClipAtPoint(clip, transform, 1f);
    }

    public virtual void SetClip(AudioClip clip)
    {
        this.clip = clip;
    }
}
